using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CommMutListCreateEffect : ICommMutListEffect {
  public readonly int id;
  public CommMutListCreateEffect(int id) {
    this.id = id;
  }
  int ICommMutListEffect.id => id;
  public void visitICommMutListEffect(ICommMutListEffectVisitor visitor) {
    visitor.visitCommMutListCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCommMutListEffect(this);
  }
}

}
