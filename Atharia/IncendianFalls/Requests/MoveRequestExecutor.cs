using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class MoveExecutor {
    Superstate superstate;
    Game game;
    Location destination;

    public MoveExecutor(
        Superstate superstate,
        Game game,
        Location destination) {
      this.superstate = superstate;
      this.game = game;
      this.destination = destination;
    }

    public void Execute() {
      Actions.Step(game, superstate, game.player, destination, false, true);
      //GameLoop.NoteUnitActed(game, game.player);
    }
  }

  public class MoveRequestExecutor {
    //public static void Continue(
    //    SSContext context,
    //    Game game,
    //    Superstate superstate) {
    //  var steps = superstate.navigatingState.path;
    //  Asserts.Assert(steps.Count > 0, "No steps!");
    //  var nextStep = steps[0];
    //  steps.RemoveAt(0);
    //  Asserts.Assert(Actions.CanStep(game, superstate, game.player, nextStep), "Can't step!");
    //  Move(context, game, superstate, nextStep);
    //  if (steps.Count == 0) {
    //    superstate.navigatingState = null;
    //  }
    //}


    public static void Move(
        SSContext context,
        Game game,
        Superstate superstate,
        Location destination) {
      Asserts.Assert(Actions.CanStep(game, superstate, game.player, destination));

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(new MoveRequest(game.id, destination).AsIRequest());
      Actions.Step(game, superstate, game.player, destination, false, true);

      //GameLoop.ContinueAfterUnitAction(
      //  context,
      //    game,
      //    superstate,
      //    new PauseCondition(0));
    }

    public static string Execute(
        SSContext context,
        Superstate superstate,
        MoveRequest request) {
      int gameId = request.gameId;
      Location destination = request.destination;
      var game = context.root.GetGame(gameId);

      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        return "Error: Unexpected player input!";
      }
      if (!game.actingUnit.Exists()) {
        return "Error: No player!";
      }
      if (!game.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit!";
      }
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot move while time shifting!";
      }
      if (game.player.nextActionTime != game.time) {
        return "Error: Not player's time to act!";
      }
      Asserts.Assert(game.WaitingOnPlayerInput());

      //if (superstate.navigatingState != null) {
      //  return "Error: Cannot move while already navigating!";
      //}
      if (destination == game.player.location) {
        return "Already there!";
      }

      if (Actions.CanStep(game, superstate, game.player, destination)) {
        Move(context, game, superstate, destination);
      } else {
        return "Too far away!";
        //var terrain = game.level.terrain;
        //var steps =
        //    AStarExplorer.Go(
        //        terrain.pattern,
        //        game.player.location,
        //        destination,
        //        game.level.ConsiderCornersAdjacent(),
        //        (Location from, Location to) => {
        //          return terrain.tiles.ContainsKey(to) &&
        //              terrain.tiles[to].IsWalkable() &&
        //              terrain.GetElevationDifference(from, to) <= 2;
        //        });
        //if (steps.Count == 0) {
        //  return "Can't go there!";
        //}
        //if (!Actions.CanStep(game, superstate, game.player, steps[0])) {
        //  return "Can't step that way!";
        //}
        //superstate.navigatingState = new Superstate.NavigatingState(steps);
        //Continue(context, game, superstate);
      }

      return "";
    }
  }
}
