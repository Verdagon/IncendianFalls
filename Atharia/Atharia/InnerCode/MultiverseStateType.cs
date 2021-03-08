using System;
using Atharia.Model;

namespace Atharia.Model {
  public enum MultiverseStateType {
    kBetweenUnits = 0,
    kBeforePlayerInput = 4,
    kTimeshiftingBackward = 6,
    kTimeshiftingCloneMoving = 7,
    kTimeshiftingAfterCloneMoved = 8,
  }
}
