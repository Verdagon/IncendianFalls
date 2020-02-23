using System;
using System.Collections.Generic;
using Atharia.Model;

namespace Atharia.Model {
  public class Superstate {
    public class TimeShiftingState {
      // A root with the incarnation that is the future of this timeline. We maintain this
      // so that when we finish timeshifting backwards, we can read the future player and
      // bring them into the past/present.
      public Root future;

      // If future non-null, this is the index of the turn we're aiming at.
      // If the last turn in turnsIncludingPresent is this index, we're done timeshifting.
      public int targetAnchorTurnIndex;

      public Location targetAnchorLocation;

      // True while we're rewinding.
      // False while we're waiting for the clone to move out of the way.
      public bool rewinding;

      public TimeShiftingState(
          Root future,
          int targetAnchorTurnIndex,
          Location targetAnchorLocation,
          bool rewinding) {
        this.future = future;
        this.targetAnchorTurnIndex = targetAnchorTurnIndex;
        this.targetAnchorLocation = targetAnchorLocation;
        this.rewinding = rewinding;
      }
    }

    public class NavigatingState {
      public readonly List<Location> path;

      public NavigatingState(List<Location> path) {
        this.path = path;
      }
    }

    public Game game;

    // Views
    public LevelSuperstate levelSuperstate;

    // Extra-model state

    // 0th element is the first turn of the game.
    public List<RootIncarnation> previousTurns;
    // The requests to get to each turn. There will be one less element in here
    // than in previousTurns. For example, if previousTurns has 2, this will have 1.
    public List<IRequest> requests;
    public List<int> anchorTurnIndices;

    // If this is non-null, we are currently timeshifting.
    public TimeShiftingState timeShiftingState;

    public NavigatingState navigatingState;

    public Superstate(
        Game game,
        LevelSuperstate liveUnitByLocationMap,
        List<RootIncarnation> previousTurns,
        List<IRequest> requests,
        List<int> anchorTurnIndices,
        TimeShiftingState timeShiftingState,
        NavigatingState navigatingState) {
      this.game = game;
      this.levelSuperstate = liveUnitByLocationMap;
      this.previousTurns = previousTurns;
      this.requests = requests;
      this.anchorTurnIndices = anchorTurnIndices;
      this.timeShiftingState = timeShiftingState;
      this.navigatingState = navigatingState;
    }

    public MultiverseStateType GetStateType() {
      var executionState = game.executionState;
      var player = game.player;
      if (timeShiftingState != null) {
        if (timeShiftingState.rewinding) {
          return MultiverseStateType.kTimeshiftingBackward;
        } else {
          if (executionState.actingUnit.Exists()) {
            Asserts.Assert(!executionState.remainingPreActingUnitComponents.Exists());
            Asserts.Assert(!executionState.remainingPostActingUnitComponents.Exists());
            return MultiverseStateType.kTimeshiftingCloneMoving;
          } else {
            // If this assert fails, that means the old player is still on the anchor,
            // which is a very bad thing.
            Asserts.Assert(
                !game.player.Exists() ||
                game.player.location != timeShiftingState.targetAnchorLocation);
            Asserts.Assert(
                !levelSuperstate.ContainsKey(
                    timeShiftingState.targetAnchorLocation));

            return MultiverseStateType.kTimeshiftingAfterCloneMoved;
          }
        }
      } else {
        switch (game.GetStateType()) {
          case WorldStateType.kAfterUnitAction:
            return MultiverseStateType.kAfterUnitAction;
          case WorldStateType.kBeforeEnemyAction:
            return MultiverseStateType.kBeforeEnemyAction;
          case WorldStateType.kBeforePlayerInput:
            if (navigatingState != null) {
              return MultiverseStateType.kBeforePlayerResume;
            } else {
              return MultiverseStateType.kBeforePlayerInput;
            }
          case WorldStateType.kBetweenUnits:
            return MultiverseStateType.kBetweenUnits;
          case WorldStateType.kPostActingDetail:
            return MultiverseStateType.kPostActingDetail;
          case WorldStateType.kPreActingDetail:
            return MultiverseStateType.kPreActingDetail;
          default:
            throw new Exception("Unknown state " + game.GetStateType());
        }
      }
    }
  }
}
