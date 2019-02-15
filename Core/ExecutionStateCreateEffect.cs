using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateCreateEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly ExecutionStateIncarnation incarnation;
  public ExecutionStateCreateEffect(
      int id,
      ExecutionStateIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IExecutionStateEffect.id => id;
  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateCreateEffect(this);
  }
}
       
}
