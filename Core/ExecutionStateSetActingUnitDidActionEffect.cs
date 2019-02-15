using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetActingUnitDidActionEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly bool newValue;
  public ExecutionStateSetActingUnitDidActionEffect(
      int id,
      bool newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetActingUnitDidActionEffect(this);
  }
}
           
}
