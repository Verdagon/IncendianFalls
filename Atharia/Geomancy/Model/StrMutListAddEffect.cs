using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct StrMutListAddEffect : IStrMutListEffect {
  public readonly int id;
  public readonly int index;
  public readonly string element;
  public StrMutListAddEffect(int id, int index, string element) {
    this.id = id;
    this.index = index;
    this.element = element;
  }
  int IStrMutListEffect.id => id;
  public void visitIStrMutListEffect(IStrMutListEffectVisitor visitor) {
    visitor.visitStrMutListAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStrMutListEffect(this);
  }
}

}
