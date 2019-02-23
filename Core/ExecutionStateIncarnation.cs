using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ExecutionStateIncarnation {
  public  int actingUnit;
  public  bool actingUnitDidAction;
  public  int remainingPreActingUnitComponents;
  public  int remainingPostActingUnitComponents;
  public ExecutionStateIncarnation(
      int actingUnit,
      bool actingUnitDidAction,
      int remainingPreActingUnitComponents,
      int remainingPostActingUnitComponents) {
    this.actingUnit = actingUnit;
    this.actingUnitDidAction = actingUnitDidAction;
    this.remainingPreActingUnitComponents = remainingPreActingUnitComponents;
    this.remainingPostActingUnitComponents = remainingPostActingUnitComponents;
  }
}

}
