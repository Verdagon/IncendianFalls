using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetActingUnitEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly Unit newValue;
  public ExecutionStateSetActingUnitEffect(
      int id,
      Unit newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetActingUnitEffect(this);
  }
}
           
}
