using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetRemainingPreActingUnitComponentsEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly IPreActingUnitComponentMutBunch newValue;
  public ExecutionStateSetRemainingPreActingUnitComponentsEffect(
      int id,
      IPreActingUnitComponentMutBunch newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetRemainingPreActingUnitComponentsEffect(this);
  }
}
           
}
