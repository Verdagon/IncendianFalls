using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateSetRemainingPostActingUnitComponentsEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly int newValue;
  public ExecutionStateSetRemainingPostActingUnitComponentsEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IExecutionStateEffect.id => id;

  public void visitIExecutionStateEffect(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateSetRemainingPostActingUnitComponentsEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExecutionStateEffect(this);
  }
}

}
