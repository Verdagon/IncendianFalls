using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ItemTTCIncarnation : IItemTTCEffectVisitor {
  public readonly int item;
  public ItemTTCIncarnation(
      int item) {
    this.item = item;
  }
  public ItemTTCIncarnation Copy() {
    return new ItemTTCIncarnation(
item    );
  }

  public void visitItemTTCCreateEffect(ItemTTCCreateEffect e) {}
  public void visitItemTTCDeleteEffect(ItemTTCDeleteEffect e) {}

  public void ApplyEffect(IItemTTCEffect effect) { effect.visitIItemTTCEffect(this); }
}

}
