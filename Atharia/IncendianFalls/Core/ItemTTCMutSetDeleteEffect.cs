using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ItemTTCMutSetDeleteEffect : IItemTTCMutSetEffect {
  public readonly int id;
  public ItemTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IItemTTCMutSetEffect.id => id;
  public void visitIItemTTCMutSetEffect(IItemTTCMutSetEffectVisitor visitor) {
    visitor.visitItemTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitItemTTCMutSetEffect(this);
  }
}

}
