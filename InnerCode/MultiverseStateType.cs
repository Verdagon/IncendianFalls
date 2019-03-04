using System;
using Atharia.Model;

namespace Atharia.Model {
  public enum MultiverseStateType {
    kBetweenUnits = 0,
    kPreActingDetail = 1,
    kAfterUnitAction = 2,
    kBeforeEnemyAction = 3,
    kBeforePlayerInput = 4,
    kBeforePlayerResume = 9,
    kPostActingDetail = 5,
    kTimeshiftingBackward = 6,
    kTimeshiftingCloneMoving = 7,
    kTimeshiftingAfterCloneMoved = 8,
  }
}
