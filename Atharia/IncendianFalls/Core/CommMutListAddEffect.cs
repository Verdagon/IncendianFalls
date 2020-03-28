using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CommMutListAddEffect : ICommMutListEffect {
  public readonly int id;
  public readonly int index;
  public readonly int element;
  public CommMutListAddEffect(int id, int index, int element) {
    this.id = id;
    this.index = index;
    this.element = element;
  }
  int ICommMutListEffect.id => id;
  public void visitICommMutListEffect(ICommMutListEffectVisitor visitor) {
    visitor.visitCommMutListAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCommMutListEffect(this);
  }
}

}
