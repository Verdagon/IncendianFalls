using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetRemainingPreActingDetailsEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly IDetailMutList newValue;
  public ExecutionStateSetRemainingPreActingDetailsEffect(
      int id,
      IDetailMutList newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetRemainingPreActingDetailsEffect(this);
  }
}
           
}
