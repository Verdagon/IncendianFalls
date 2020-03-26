using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListAddEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public readonly int index;
  public readonly IUnitEvent element;
  public IUnitEventMutListAddEffect(int id, int index, IUnitEvent element) {
    this.id = id;
    this.index = index;
    this.element = element;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visitIIUnitEventMutListEffect(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIUnitEventMutListEffect(this);
  }
}

}
