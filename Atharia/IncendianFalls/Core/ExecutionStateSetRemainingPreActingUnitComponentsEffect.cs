using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetRemainingPreActingUnitComponentsEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly int newValue;
  public ExecutionStateSetRemainingPreActingUnitComponentsEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visitIExecutionStateEffect(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetRemainingPreActingUnitComponentsEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExecutionStateEffect(this);
  }
}

}
