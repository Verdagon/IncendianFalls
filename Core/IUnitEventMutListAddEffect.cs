using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListAddEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public readonly IUnitEvent element;
  public IUnitEventMutListAddEffect(int id, IUnitEvent element) {
    this.id = id;
    this.element = element;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visit(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListAddEffect(this);
  }
}

}
