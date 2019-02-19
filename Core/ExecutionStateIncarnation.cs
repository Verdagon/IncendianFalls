using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class ExecutionStateIncarnation {
  public int actingUnit;
  public bool actingUnitDidAction;
  public int remainingPreActingDetails;
  public int remainingPostActingDetails;
  public ExecutionStateIncarnation(
      int actingUnit,
      bool actingUnitDidAction,
      int remainingPreActingDetails,
      int remainingPostActingDetails) {
    this.actingUnit = actingUnit;
    this.actingUnitDidAction = actingUnitDidAction;
    this.remainingPreActingDetails = remainingPreActingDetails;
    this.remainingPostActingDetails = remainingPostActingDetails;
  }
  public int GetDeterministicHashCode() {
    int s = 0;
    s = s * 37 + actingUnit.GetDeterministicHashCode();
    s = s * 37 + actingUnitDidAction.GetDeterministicHashCode();
    s = s * 37 + remainingPreActingDetails.GetDeterministicHashCode();
    s = s * 37 + remainingPostActingDetails.GetDeterministicHashCode();
    return s;
  }
}

}
