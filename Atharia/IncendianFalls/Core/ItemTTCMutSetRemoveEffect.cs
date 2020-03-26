using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTTCMutSetRemoveEffect : IItemTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ItemTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IItemTTCMutSetEffect.id => id;
  public void visitIItemTTCMutSetEffect(IItemTTCMutSetEffectVisitor visitor) {
    visitor.visitItemTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitItemTTCMutSetEffect(this);
  }
}

}
