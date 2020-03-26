using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateDeleteEffect : IExecutionStateEffect {
  public readonly int id;
  public ExecutionStateDeleteEffect(int id) {
    this.id = id;
  }
  int IExecutionStateEffect.id => id;
  public void visitIExecutionStateEffect(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExecutionStateEffect(this);
  }
}

}
