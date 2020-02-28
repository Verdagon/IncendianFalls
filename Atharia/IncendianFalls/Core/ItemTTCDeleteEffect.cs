using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ItemTTCDeleteEffect : IItemTTCEffect {
  public readonly int id;
  public ItemTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IItemTTCEffect.id => id;
  public void visit(IItemTTCEffectVisitor visitor) {
    visitor.visitItemTTCDeleteEffect(this);
  }
}

}
