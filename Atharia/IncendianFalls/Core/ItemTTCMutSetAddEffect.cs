using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTTCMutSetAddEffect : IItemTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ItemTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IItemTTCMutSetEffect.id => id;
  public void visitIItemTTCMutSetEffect(IItemTTCMutSetEffectVisitor visitor) {
    visitor.visitItemTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitItemTTCMutSetEffect(this);
  }
}

}
