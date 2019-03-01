using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ExecutionStateCreateEffect : IExecutionStateEffect {
  public readonly int id;
  public ExecutionStateCreateEffect(int id) {
    this.id = id;
  }
  int IExecutionStateEffect.id => id;
  public void visit(IExecutionStateEffectVisitor visitor) {
    visitor.visitExecutionStateCreateEffect(this);
  }
}

}
