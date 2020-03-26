using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListRemoveEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public readonly int index;
  public IUnitEventMutListRemoveEffect(int id, int index) {
    this.id = id;
    this.index = index;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visitIIUnitEventMutListEffect(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIUnitEventMutListEffect(this);
  }
}

}
