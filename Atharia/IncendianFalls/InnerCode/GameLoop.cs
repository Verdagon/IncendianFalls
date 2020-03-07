using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class PauseCondition {
    public readonly bool betweenUnits;
    public PauseCondition(bool betweenUnits) {
      this.betweenUnits = betweenUnits;
    }
  }

  public class GameLoop {
    private static void flare(Game game, string message) {
      //game.root.logger.Info(message);
    }

    public static void Continue(Game game, Superstate superstate, PauseCondition pauseCondition) {
      SortedSet<int> thisSpreeStartedUnitIds = new SortedSet<int>();

      switch (game.GetStateType()) {
        case WorldStateType.kBetweenUnits:
          ContinueAtStartTurn(
              game, superstate, pauseCondition, thisSpreeStartedUnitIds);
          break;
        case WorldStateType.kAfterUnitAction:
          ContinueAfterUnitAction(
              game, superstate, pauseCondition, thisSpreeStartedUnitIds);
          break;
        case WorldStateType.kPreActingDetail:
          ContinueAfterPreActingDetail(
              game, superstate, pauseCondition, thisSpreeStartedUnitIds);
          break;
        case WorldStateType.kPostActingDetail:
          ContinueAfterPostActingDetail(
              game, superstate, pauseCondition, thisSpreeStartedUnitIds);
          break;
        case WorldStateType.kBeforeEnemyAction:
          // If we timeshifted, and just finished rewinding, the player just
          // got reclassified as a time clone, who's about to do their action.
          ContinueBeforeUnitAction(
              game, superstate, pauseCondition, thisSpreeStartedUnitIds);
          break;
        case WorldStateType.kBeforePlayerInput:
          Asserts.Assert(false, "at beforeplayerinput! " + superstate.GetStateType() + " <-");
          break;
        default:
          Asserts.Assert(false, "unknown state!");
          break;
      }
    }

    //public static void ContinueAtPlayerFollowDirective(
    //    Game game,
    //    Superstate superstate,
    //    PauseCondition pauseCondition) {
    //  SortedSet<int> thisSpreeStartedUnitIds = new SortedSet<int>();

    //  if (game.player.GetDirectiveOrNull() is MoveDirectiveUCAsIDirectiveUC move) {

    //    bool ret = PlayerAI.AI(game, superstate);
    //    if (!ret) {
    //      game.root.logger.Error("Couldn't follow move directive, bail!");
    //      game.player.ClearDirective();
    //      return;
    //    }


    //    GameLoop.NoteUnitActed(game, game.player);

    //    GameLoop.ContinueAfterUnitAction(
    //        game, superstate, pauseCondition, thisSpreeStartedUnitIds);
    //    return;
    //  } else if (game.player.GetDirectiveOrNull() is TimeScriptDirectiveUCAsIDirectiveUC tsdI) {

    //    bool ret = PlayerAI.AI(game, superstate);
    //    Asserts.Assert(ret); // TODO, implement time clone evaporating

    //    GameLoop.NoteUnitActed(game, game.player);

    //    GameLoop.ContinueAfterUnitAction(
    //        game, superstate, pauseCondition, thisSpreeStartedUnitIds);
    //    return;
    //  } else {
    //    Asserts.Assert(false);
    //    return;
    //  }
    //}

    // Called from the outside.
    public static void ContinueAtStartTurn(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(!executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
      Asserts.Assert(!executionState.actingUnitDidAction);

      StartNextUnit(game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public static void NoteUnitAppearedReadyToAct(Game game, Superstate superstate) {
      var executionState = game.executionState;
      Asserts.Assert(!executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
      Asserts.Assert(!executionState.actingUnitDidAction);

      game.executionState.actingUnit = game.player;

      Asserts.Assert(game.GetStateType() == WorldStateType.kBeforePlayerInput);
    }

    public static void NoteUnitActed(
        Game game,
        Unit unit) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
      Asserts.Assert(!executionState.actingUnitDidAction);

      executionState.actingUnitDidAction = true;

      // To be continued... via ContinueAfterUnitAction.

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Called from the outside
    public static void ContinueAfterUnitAction(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());

      StartPostActions(
          game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Only called from the inside, from ContinueAfterUnitAction, PlayerDefy, etc.
    private static void StartPostActions(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;

      var postActingDetails = IPostActingUCWeakMutBunch.New(game.root);
      foreach (var details in executionState.actingUnit.components.GetAllIPostActingUC()) {
        postActingDetails.Add(details);
      }
      executionState.remainingPostActingUnitComponents = postActingDetails;

      DoNextPostActingDetailOrContinue(
          game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Can be called from StartPostActions or ContinueAfterPreActingDetail.
    private static void DoNextPostActingDetailOrContinue(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      var unit = executionState.actingUnit;
      var remainingPostActingDetails = executionState.remainingPostActingUnitComponents;
      Asserts.Assert(remainingPostActingDetails.Exists());
      Asserts.Assert(executionState.actingUnitDidAction);

      while (remainingPostActingDetails.Count > 0) {
        var detail = remainingPostActingDetails.GetArbitrary();
        remainingPostActingDetails.Remove(detail);
        if (!detail.Exists()) {
          continue;
        }
        bool ret = detail.PostAct(game, superstate, executionState.actingUnit);
        //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (ret) {
          return; // To be continued... via ContinueAfterPostActingDetail.
        }
      }
      // There were no existing remaining pre-acting details. Go on to the unit's acting.
      executionState.remainingPostActingUnitComponents.Destruct();

      // Figure out a way for a trigger to interrupt, and then we could resume after that trigger.
      // Similar to what we did with the post acting unit components.
      // This is so we can trigger a cinematic or camera movement and pause simulation while it's
      // going.
      // Presumably we'd need a trigger to resume simulation...
      var presenceTriggers = game.level.terrain.tiles[unit.location].components.GetAllIPresenceTriggerTTC();
      Asserts.Assert(presenceTriggers.Count <= 1); // dont support multiple yet. we could just fire both of them at once.
      // but we should probably have some sort of stop/resume between each trigger.
      foreach (var presenceTrigger in presenceTriggers) {
        presenceTrigger.Trigger(game, superstate, unit, unit.location);
      }

      var wasPlayer = game.player.Exists() && unit.NullableIs(game.player);
      executionState.actingUnit = new Unit(game.root, 0);
      executionState.actingUnitDidAction = false;

      if (wasPlayer) {
        foreach (var locationAndActingTTC in superstate.levelSuperstate.GetAllActingTTCs()) {
          Asserts.Assert(locationAndActingTTC.Value.Exists(), "exists curiosity");
          locationAndActingTTC.Value.Act(game, superstate, locationAndActingTTC.Key);
        }
      }

      if (pauseCondition.betweenUnits) {
        return; // To be continued... via ContinueBeforeEnemyAction.
      } else {
        StartNextUnit(game, superstate, pauseCondition, thisSpreeStartedUnitIds);
      }

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Called from the outside, after we do a pre-acting detail.
    public static void ContinueAfterPostActingDetail(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      DoNextPostActingDetailOrContinue(
          game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Called from ContinueAtStartTurn or ContinueAtNextPostActingDetail.
    private static void StartNextUnit(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(!executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
      Asserts.Assert(!executionState.actingUnitDidAction);

      if (game.level.units.Count == 0) {
        //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        return;
      }

      Unit nextUnit;
      while (true) {
        nextUnit = Utils.GetNextActingUnit(game);

        if (thisSpreeStartedUnitIds.Contains(nextUnit.id)) {
          return; // To be continued... via a ContinueAtStartTurn.
        }
        thisSpreeStartedUnitIds.Add(nextUnit.id);

        game.level.time = nextUnit.nextActionTime;
        game.time = nextUnit.nextActionTime;

        if (nextUnit.alive == false) {
          if (nextUnit.NullableIs(game.player)) {
            return; // To be continued... via ContinueAtStartTurn.
          } else {
            game.level.units.Remove(nextUnit);
            nextUnit.Destruct();
            continue;
          }
        }
        break;
      }

      executionState.actingUnit = nextUnit;

      StartPreActions(game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Never called from the outside, always called by StartNextUnit
    private static void StartPreActions(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {

      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());

      var preActingDetails = IPreActingUCWeakMutBunch.New(game.root);
      foreach (var details in executionState.actingUnit.components.GetAllIPreActingUC()) {
        preActingDetails.Add(details);
      }
      executionState.remainingPreActingUnitComponents = preActingDetails;

      DoNextPreActingDetailOrContinue(
          game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Can be called from StartPreActions or ContinueAfterPreActingDetail.
    private static void DoNextPreActingDetailOrContinue(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      var unit = executionState.actingUnit;
      var remainingPreActingDetails = executionState.remainingPreActingUnitComponents;
      Asserts.Assert(remainingPreActingDetails.Exists());

      while (remainingPreActingDetails.Count > 0) {
        var detail = remainingPreActingDetails.GetArbitrary();
        remainingPreActingDetails.Remove(detail);
        if (!detail.Exists()) {
          continue;
        }
        bool ret = detail.PreAct(game, superstate, executionState.actingUnit);
        //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        if (ret) {
          return; // To be continued... via ContinueAfterPreActingDetail.
        }
      }

      // There were no existing remaining pre-acting details. Go on to the unit's action.
      executionState.remainingPreActingUnitComponents.Destruct();

      DoUnitAction(
          game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // Called from the oustide, after we do a pre-acting detail.
    public static void ContinueAfterPreActingDetail(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      DoNextPreActingDetailOrContinue(
          game, superstate, pauseCondition, thisSpreeStartedUnitIds);

      //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    public static void ContinueBeforeUnitAction(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      Asserts.Assert(game.GetStateType() == WorldStateType.kBeforeEnemyAction);

      DoUnitAction(
        game, superstate, pauseCondition, thisSpreeStartedUnitIds);
    }

    private static void DoUnitAction(
        Game game,
        Superstate superstate,
        PauseCondition pauseCondition,
        SortedSet<int> thisSpreeStartedUnitIds) {
      //flare(game, System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
      Asserts.Assert(!executionState.actingUnitDidAction);
      var unit = executionState.actingUnit;

      if (unit.NullableIs(game.player)) {
        // It's the player turn, we don't do any AI for it.
        //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        return; // To be continued... via PlayerAttack, PlayerMove, etc.
      } else {
        bool ret = EnemyAI.AI(game, superstate, unit);
        executionState.actingUnitDidAction = true;
        //flare(game, "/" + System.Reflection.MethodBase.GetCurrentMethod().Name);

        if (ret) {
          return; // To be continued... via ContinueAfterUnitAction.
        }
      }

      ContinueAfterUnitAction(
          game, superstate, pauseCondition, thisSpreeStartedUnitIds);
    }

  }
}
