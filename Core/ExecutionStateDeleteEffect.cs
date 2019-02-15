using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateDeleteEffect : IExecutionStateEffect {
  public readonly int id;
  public ExecutionStateDeleteEffect(int id) {
    this.id = id;
  }
  int IExecutionStateEffect.id => id;
  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateDeleteEffect(this);
  }
}
       
}
