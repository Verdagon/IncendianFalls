using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IRequestMutListRemoveEffect : IIRequestMutListEffect {
  public readonly int id;
  public readonly int index;
  public IRequestMutListRemoveEffect(int id, int index) {
    this.id = id;
    this.index = index;
  }
  int IIRequestMutListEffect.id => id;
  public void visitIIRequestMutListEffect(IIRequestMutListEffectVisitor visitor) {
    visitor.visitIRequestMutListRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIRequestMutListEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
