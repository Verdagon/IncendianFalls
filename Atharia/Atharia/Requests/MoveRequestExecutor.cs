using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class MoveRequestExecutor {
    public static void Move(
        SSContext context,
        Game game,
        Superstate superstate,
        Location destination) {
      Asserts.Assert(superstate.levelSuperstate.CanHop(game.player.location, destination, true));

      superstate.previousTurns.Add(context.root.Snapshot());
      superstate.requests.Add(new MoveRequest(game.id, destination).AsIRequest());
      Actions.Hop(game, superstate, game.player, destination, true);

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

      if (!game.actingUnit.Exists()) {
        return "Error: No player!";
      }
      if (!game.actingUnit.Is(game.player)) {
        return "Error: Player not next acting unit!";
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

      if (superstate.levelSuperstate.CanHop(game.player.location, destination, true)) {
        Move(context, game, superstate, destination);
      } else {
        return "Too far away!";
        //var terrain = game.level.terrain;
        //var steps =
        //    AStarExplorer.Go(
        //        terrain.pattern,
        //        game.player.location,
        //        destination,
        //        game.level.terrain.considerCornersAdjacent,
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
