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
      Actions.Step(game, superstate, game.player, destination, false);
      GameLoop.NoteUnitActed(game, game.player);
    }
  }

  public class MoveRequestExecutor {
    public static void Continue(
        SSContext context,
        Game game,
        Superstate superstate) {
      var steps = superstate.navigatingState.path;
      Asserts.Assert(steps.Count > 0);
      var nextStep = steps[0];
      steps.RemoveAt(0);
      Asserts.Assert(Actions.CanStep(game, superstate, game.player, nextStep));
      Move(context, game, superstate, nextStep);
      if (steps.Count == 0) {
        superstate.navigatingState = null;
      }
    }


    public static void Move(
        SSContext context,
        Game game,
        Superstate superstate,
        Location destination) {
      Asserts.Assert(Actions.CanStep(game, superstate, game.player, destination));

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(new MoveRequest(game.id, destination).AsIRequest());
      Actions.Step(game, superstate, game.player, destination, false);

      GameLoop.NoteUnitActed(game, game.player);

      GameLoop.ContinueAfterUnitAction(
          game,
          superstate,
          new PauseCondition(false),
          new SortedSet<int>());
    }

    public static string Execute(
        SSContext context,
        Superstate superstate,
        MoveRequest request) {
      int gameId = request.gameId;
      Location destination = request.destination;
      var game = context.root.GetGame(gameId);

      EventsClearer.Clear(game);

      if (superstate.GetStateType() != MultiverseStateType.kBeforePlayerInput) {
        return "Error: Unexpected player input!";
      }
      if (!game.executionState.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit! (a)";
      }
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot move while time shifting!";
      }
      if (superstate.navigatingState != null) {
        return "Error: Cannot move while already navigating!";
      }
      if (destination == game.player.location) {
        return "Already there!";
      }

      if (Actions.CanStep(game, superstate, game.player, destination)) {
        Move(context, game, superstate, destination);
      } else {
        var terrain = game.level.terrain;
        var steps =
            AStarExplorer.Go(
                terrain.pattern,
                game.player.location,
                destination,
                game.level.ConsiderCornersAdjacent(),
                (Location from, Location to) => {
                  return terrain.tiles.ContainsKey(to) &&
                      terrain.tiles[to].walkable &&
                      terrain.GetElevationDifference(from, to) <= 2;
                });
        if (steps.Count == 0) {
          return "Can't go there!";
        }
        superstate.navigatingState = new Superstate.NavigatingState(steps);
        Continue(context, game, superstate);
      }

      return "";
    }
  }
}
