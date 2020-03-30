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
  public void visitIItemTTCEffect(IItemTTCEffectVisitor visitor) {
    visitor.visitItemTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitItemTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
