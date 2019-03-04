using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTTCMutSetAddEffect : IItemTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ItemTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IItemTTCMutSetEffect.id => id;
  public void visit(IItemTTCMutSetEffectVisitor visitor) {
    visitor.visitItemTTCMutSetAddEffect(this);
  }
}

}
