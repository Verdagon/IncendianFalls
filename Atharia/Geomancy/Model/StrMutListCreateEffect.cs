using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct StrMutListCreateEffect : IStrMutListEffect {
  public readonly int id;
  public StrMutListCreateEffect(int id) {
    this.id = id;
  }
  int IStrMutListEffect.id => id;
  public void visitIStrMutListEffect(IStrMutListEffectVisitor visitor) {
    visitor.visitStrMutListCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStrMutListEffect(this);
  }
}

}
