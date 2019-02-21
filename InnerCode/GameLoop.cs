using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atharia.Model;

namespace IncendianFalls {
  public class GameLoop {
    private static void flare(string message) {
       //Console.WriteLine(message);
    }

    // Called from the outside.
    public static void ContinueAtStartTurn(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(!executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());

      StartNextUnit(game, liveUnitByLocationMap);
    }

    public static void NoteUnitActed(Game game, Unit unit) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
      Asserts.Assert(!executionState.actingUnitDidAction);

      executionState.actingUnitDidAction = true;

      // To be continued... via ContinueAfterUnitAction.
    }

    // Called from the outside
    public static void ContinueAfterUnitAction(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());

      StartPostActions(game, liveUnitByLocationMap);
    }

    // Only called from the inside, from ContinueAfterUnitAction, PlayerDefend, etc.
    private static void StartPostActions(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;

      var postActingDetails = IPostActingUnitComponentMutBunch.New(game.root);
      foreach (var details in executionState.actingUnit.components.GetAllIPostActingUnitComponent()) {
        postActingDetails.Add(details);
      }
      executionState.remainingPostActingUnitComponents = postActingDetails;

      DoNextPostActingDetailOrContinue(game, liveUnitByLocationMap);
    }

    // Can be called from StartPostActions or ContinueAfterPreActingDetail.
    private static void DoNextPostActingDetailOrContinue(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      var unit = executionState.actingUnit;
      var remainingPostActingDetails = executionState.remainingPostActingUnitComponents;
      Asserts.Assert(remainingPostActingDetails.Exists());

      while (remainingPostActingDetails.Count > 0) {
        var detail = remainingPostActingDetails.GetArbitrary();
        remainingPostActingDetails.Remove(detail);
        if (!detail.Exists()) {
          continue;
        }
        detail.PostAct(executionState.actingUnit);
        return; // To be continued... via ContinueAfterPostActingDetail.
      }
      // There were no existing remaining pre-acting details. Go on to the unit's acting.
      executionState.remainingPostActingUnitComponents.Delete();

      executionState.actingUnit = new Unit(game.root, 0);

      StartNextUnit(game, liveUnitByLocationMap);
    }

    // Called from the outside, after we do a pre-acting detail.
    public static void ContinueAfterPostActingDetail(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      DoNextPostActingDetailOrContinue(game, liveUnitByLocationMap);
    }

    // Called from ContinueAtStartTurn or ContinueAtNextPostActingDetail.
    private static void StartNextUnit(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(!executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());

      if (game.level.units.Count == 0) {
        return;
      }


      var nextUnit = Utils.GetNextActingUnit(game);
      game.time = nextUnit.nextActionTime;

      if (nextUnit.alive == false) {
        game.level.units.Remove(nextUnit);
        nextUnit.Destructor();
        return; // To be continued... via ContinueAtStartTurn.
      }

      executionState.actingUnit = nextUnit;
      executionState.actingUnitDidAction = false;

      StartPreActions(game, liveUnitByLocationMap);
    }

    // Never called from the outside, always called by StartNextUnit
    private static void StartPreActions(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());

      var preTurnActingDetails = IPreActingUnitComponentMutBunch.New(game.root);
      foreach (var details in executionState.actingUnit.components.GetAllIPreActingUnitComponent()) {
        preTurnActingDetails.Add(details);
      }
      executionState.remainingPreActingUnitComponents = preTurnActingDetails;

      DoNextPreActingDetailOrContinue(game, liveUnitByLocationMap);
    }

    // Can be called from StartPreActions or ContinueAfterPreActingDetail.
    private static void DoNextPreActingDetailOrContinue(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

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
        detail.PreAct(executionState.actingUnit);
        return; // To be continued... via ContinueAfterPreActingDetail.
      }

      // There were no existing remaining pre-acting details. Go on to the unit's action.
      executionState.remainingPreActingUnitComponents.Delete();

      DoUnitAction(game, liveUnitByLocationMap);
    }

    // Called from the oustide, after we do a pre-acting detail.
    public static void ContinueAfterPreActingDetail(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      DoNextPreActingDetailOrContinue(game, liveUnitByLocationMap);
    }

    private static void DoUnitAction(Game game, LiveUnitByLocationMap liveUnitByLocationMap) {
      flare(System.Reflection.MethodBase.GetCurrentMethod().Name);

      var executionState = game.executionState;
      Asserts.Assert(executionState.actingUnit.Exists());
      Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
      Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
      Asserts.Assert(!executionState.actingUnitDidAction);
      var unit = executionState.actingUnit;

      if (unit.mp < unit.maxMp) {
        unit.mp = unit.mp + 1;
      }

      if (unit.Is(game.player)) {
        // It's the player turn, so we don't do any AI for it.
        return; // To be continued... via PlayerAttack, PlayerMove, etc.
      } else {
        EnemyAI.AI(game, liveUnitByLocationMap, unit);
        executionState.actingUnitDidAction = true;
        return; // To be continued... via ContinueAfterUnitAction.
      }
    }

  }
}
