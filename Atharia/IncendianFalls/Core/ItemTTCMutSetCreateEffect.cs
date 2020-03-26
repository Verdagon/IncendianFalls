using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTTCMutSetCreateEffect : IItemTTCMutSetEffect {
  public readonly int id;
  public ItemTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IItemTTCMutSetEffect.id => id;
  public void visitIItemTTCMutSetEffect(IItemTTCMutSetEffectVisitor visitor) {
    visitor.visitItemTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitItemTTCMutSetEffect(this);
  }
}

}
