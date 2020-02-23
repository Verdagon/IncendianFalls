using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct StrMutListAddEffect : IStrMutListEffect {
  public readonly int id;
  public readonly string element;
  public StrMutListAddEffect(int id, string element) {
    this.id = id;
    this.element = element;
  }
  int IStrMutListEffect.id => id;
  public void visit(IStrMutListEffectVisitor visitor) {
    visitor.visitStrMutListAddEffect(this);
  }
}

}
