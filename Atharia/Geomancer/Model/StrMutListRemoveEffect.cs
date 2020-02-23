using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public struct StrMutListRemoveEffect : IStrMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public StrMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IStrMutListEffect.id => id;
  public void visit(IStrMutListEffectVisitor visitor) {
    visitor.visitStrMutListRemoveEffect(this);
  }
}

}
