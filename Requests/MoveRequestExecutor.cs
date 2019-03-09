using System;
using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class MoveExecutor {
    Superstate superstate;
    Game game;
    List<Location> steps;

    public MoveExecutor(
        Superstate superstate,
        Game game,
        List<Location> steps) {
      this.superstate = superstate;
      this.game = game;
      this.steps = steps;
    }

    public void Execute() {
      var path = game.root.EffectLocationMutListCreate(steps);
      var directive = game.root.EffectMoveDirectiveUCCreate(path);
      game.player.ReplaceDirective(directive.AsIDirectiveUC());

      if (!PlayerAI.AI(game, superstate)) {
        Asserts.Assert(false);
      }

      GameLoop.NoteUnitActed(game, game.player);
    }
  }

  public class MoveRequestExecutor {

    public static MoveExecutor PrepareToMove(
        Superstate superstate,
        Game game,
        Location destination) {
      Asserts.Assert(game.player.Exists());
      Asserts.Assert(game.player.alive);

      if (destination == game.player.location) {
        game.root.logger.Error("Already there!");
        return null;
      }

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
        game.root.logger.Error("No path!");
        return null;
      }

      if (!Actions.CanStep(game, superstate, game.player, steps[0])) {
        game.root.logger.Info("Blocked!");
        return null;
      }

      return new MoveExecutor(superstate, game, steps);
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
      //if (!game.player.Is(Utils.GetNextActingUnit(game))) {
      //  return "Error: Player not next acting unit! (b)";
      //}
      if (superstate.timeShiftingState != null) {
        return "Error: Cannot move while time shifting!";
      }

      superstate.previousTurns.Add(context.root.Snapshot());
      game.lastPlayerRequest = request.AsIRequest();

      var moveExecutor = PrepareToMove(superstate, game, destination);

      if (moveExecutor == null) {
        return "Could not move there!";
      }
      moveExecutor.Execute();

      return "";
    }
  }
}
