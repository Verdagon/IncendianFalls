using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemMutBunchAddEffect : IIItemMutBunchEffect {
  public readonly int id;
  public readonly int elementId;
  public IItemMutBunchAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIItemMutBunchEffect.id => id;
  public void visit(IIItemMutBunchEffectVisitor visitor) {
    visitor.visitIItemMutBunchAddEffect(this);
  }
}

}
