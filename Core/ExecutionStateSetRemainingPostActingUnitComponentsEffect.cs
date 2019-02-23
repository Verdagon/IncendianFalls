using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetRemainingPostActingUnitComponentsEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly IPostActingUCMutBunch newValue;
  public ExecutionStateSetRemainingPostActingUnitComponentsEffect(
      int id,
      IPostActingUCMutBunch newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetRemainingPostActingUnitComponentsEffect(this);
  }
}
           
}
