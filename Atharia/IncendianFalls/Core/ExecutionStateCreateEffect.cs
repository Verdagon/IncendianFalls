using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateCreateEffect : IExecutionStateEffect {
  public readonly int id;
  public readonly ExecutionStateIncarnation incarnation;
  public ExecutionStateCreateEffect(int id, ExecutionStateIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IExecutionStateEffect.id => id;
  public void visitIExecutionStateEffect(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitExecutionStateEffect(this);
  }
}

}
