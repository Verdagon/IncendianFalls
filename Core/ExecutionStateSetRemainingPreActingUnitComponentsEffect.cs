using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetRemainingPreActingUnitComponentsEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly IPreActingUCWeakMutBunch newValue;
  public ExecutionStateSetRemainingPreActingUnitComponentsEffect(
      int id,
      IPreActingUCWeakMutBunch newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetRemainingPreActingUnitComponentsEffect(this);
  }
}

}
