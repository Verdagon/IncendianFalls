using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemMutBunchRemoveEffect : IIItemMutBunchEffect {
  public readonly int id;
  public readonly int elementId;
  public IItemMutBunchRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIItemMutBunchEffect.id => id;
  public void visit(IIItemMutBunchEffectVisitor visitor) {
    visitor.visitIItemMutBunchRemoveEffect(this);
  }
}

}
