using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ItemTTCCreateEffect : IItemTTCEffect {
  public readonly int id;
  public readonly ItemTTCIncarnation incarnation;
  public ItemTTCCreateEffect(int id, ItemTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IItemTTCEffect.id => id;
  public void visitIItemTTCEffect(IItemTTCEffectVisitor visitor) {
    visitor.visitItemTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitItemTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
