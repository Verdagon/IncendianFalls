using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class GameLoop {
    public static void Continue(
        IncendianFalls.SSContext context, Game game, Superstate superstate) {
      ContinueBetweenUnits(context, game, superstate);
    }

    // Called from the outside
    public static void ContinueAfterUnitAction(
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      Asserts.Assert(game.actingUnit.Exists());

      var remainingPostActingUnitComponents = IPostActingUCWeakMutBunch.New(game.root);
      foreach (var details in game.actingUnit.components.GetAllIPostActingUC()) {
        remainingPostActingUnitComponents.Add(details);
      }
      while (remainingPostActingUnitComponents.Count > 0) {
        var detail = remainingPostActingUnitComponents.GetArbitrary();
        remainingPostActingUnitComponents.Remove(detail);
        if (!detail.Exists()) {
          continue;
        }
        if (!game.actingUnit.Alive()) {
          continue;
        }
        detail.PostAct(game, superstate, game.actingUnit);
      }
      // There were no existing remaining pre-acting details. Go on to the unit's acting.
      remainingPostActingUnitComponents.Destruct();

      if (game.actingUnit.Alive()) {
        // Figure out a way for a trigger to interrupt, and then we could resume after that trigger.
        // Similar to what we did with the post acting unit components.
        // This is so we can trigger a cinematic or camera movement and pause simulation while it's
        // going.
        // Presumably we'd need a trigger to resume simulation...
        var presenceTriggers = game.level.terrain.tiles[game.actingUnit.location].components.GetAllIPresenceTriggerTTC();
        Asserts.Assert(presenceTriggers.Count <= 1); // dont support multiple yet. we could just fire both of them at once.
        // but we should probably have some sort of stop/resume between each trigger.
        foreach (var presenceTrigger in presenceTriggers) {
          presenceTrigger.Trigger(context, game, superstate, game.actingUnit, game.actingUnit.location);
        }
      }

      var wasPlayer = game.player.Exists() && game.actingUnit.NullableIs(game.player);
      game.actingUnit = Unit.Null;

      if (wasPlayer) {
        foreach (var locationAndActingTTC in superstate.levelSuperstate.GetAllActingTTCs()) {
          Asserts.Assert(locationAndActingTTC.Value.Exists(), "exists curiosity");
          locationAndActingTTC.Value.Act(game, superstate, locationAndActingTTC.Key);
        }
      }

      if (game.pauseBeforeNextUnit) {
        game.pauseBeforeNextUnit = false;
        return;
      }

      if (!game.player.Exists() || !game.player.Alive()) {
        // Suspend if there's no player, so we dont infinite loop.
        return;
      }
      ContinueBetweenUnits(context, game, superstate);
    }

    public static void ContinueBetweenUnits(
        IncendianFalls.SSContext context,
        Game game,
        Superstate superstate) {
      Asserts.Assert(!game.actingUnit.Exists());

      Unit nextUnit;
      // Keep pruning out dead units until we find something that can move or we run out of units.
      while (true) {
        if (game.level.units.Count == 0) {
          return;
        }

        nextUnit = Utils.GetNextActingUnit(game);

        game.level.time = nextUnit.nextActionTime;
        game.time = nextUnit.nextActionTime;

        if (nextUnit.Alive() == false) {
          bool playerDying = nextUnit.NullableIs(game.player);

          foreach (var deathPreactor in nextUnit.components.GetAllIDeathPreReactor()) {
            deathPreactor.BeforeDeath(game, superstate, nextUnit);
          }

          if (superstate.levelSuperstate.LocationContainsUnit(nextUnit.location)) {
            // curiosity, we might need a:
            game.root.logger.Warning("next unit isnt alive, but doesnt exist in superstate?");
          }
          //superstate.levelSuperstate.RemoveUnit(nextUnit);
          game.level.units.Remove(nextUnit);
          nextUnit.Destruct();

          if (playerDying) {
            return;
          }

          continue;
        }
        break;
      }

      game.actingUnit = nextUnit;

      // todo: replace with a foreach and addall
      var remainingPreActingUnitComponents = IPreActingUCWeakMutBunch.New(game.root);
      foreach (var details in game.actingUnit.components.GetAllIPreActingUC()) {
        remainingPreActingUnitComponents.Add(details);
      }
      while (remainingPreActingUnitComponents.Count > 0) {
        var detail = remainingPreActingUnitComponents.GetArbitrary();
        remainingPreActingUnitComponents.Remove(detail);
        if (!game.actingUnit.Alive()) {
          continue;
        }
        if (!detail.Exists()) {
          continue;
        }
        detail.PreAct(game, superstate, game.actingUnit);
      }

      // There were no existing remaining pre-acting details. Go on to the unit's action.
      remainingPreActingUnitComponents.Destruct();

      if (game.actingUnit.NullableIs(game.player)) {
        Asserts.Assert(game.WaitingOnPlayerInput());
        // It's the player turn, we don't do any AI for it.
        //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        return; // To be continued... via PlayerAttack, PlayerMove, etc.
      } else {
        ContinueBeforeUnitAction(context, game, superstate);
      }
    }

    // Beware, the player skips this entire function.
    public static void ContinueBeforeUnitAction(
        SSContext context,
        Game game,
        Superstate superstate) {

      Asserts.Assert(game.actingUnit.Exists());
      Asserts.Assert(!game.actingUnit.NullableIs(game.player)); // Because if it was, we should be getting user input.

      while (game.actingUnit.Alive() && game.actingUnit.nextActionTime == game.time) {
        EnemyAI.AI(context, game, superstate, game.actingUnit);
      }

      ContinueAfterUnitAction(context, game, superstate);
    }
  }
}
