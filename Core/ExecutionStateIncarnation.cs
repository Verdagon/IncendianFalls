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
}

}
