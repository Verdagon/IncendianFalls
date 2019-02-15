using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitMutBunchAddEffect : IUnitMutBunchEffect {
  public readonly int id;
  public readonly int elementId;
  public UnitMutBunchAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUnitMutBunchEffect.id => id;
  public void visit(IUnitMutBunchEffectVisitor visitor) {
    visitor.visitUnitMutBunchAddEffect(this);
  }
}

}
