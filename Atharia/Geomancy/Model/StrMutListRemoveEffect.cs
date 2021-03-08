using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct StrMutListRemoveEffect : IStrMutListEffect {
  public readonly int id;
  public readonly int index;
  public StrMutListRemoveEffect(int id, int index) {
    this.id = id;
    this.index = index;
  }
  int IStrMutListEffect.id => id;
  public void visitIStrMutListEffect(IStrMutListEffectVisitor visitor) {
    visitor.visitStrMutListRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStrMutListEffect(this);
  }
}

}
