using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTTCMutSetRemoveEffect : IItemTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ItemTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IItemTTCMutSetEffect.id => id;
  public void visit(IItemTTCMutSetEffectVisitor visitor) {
    visitor.visitItemTTCMutSetRemoveEffect(this);
  }
}

}
