using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CommMutListRemoveEffect : ICommMutListEffect {
  public readonly int id;
  public readonly int index;
  public CommMutListRemoveEffect(int id, int index) {
    this.id = id;
    this.index = index;
  }
  int ICommMutListEffect.id => id;
  public void visitICommMutListEffect(ICommMutListEffectVisitor visitor) {
    visitor.visitCommMutListRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCommMutListEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
