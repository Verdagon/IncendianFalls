using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorStrongMutSetDeleteEffect : IArmorStrongMutSetEffect {
  public readonly int id;
  public ArmorStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IArmorStrongMutSetEffect.id => id;
  public void visit(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetDeleteEffect(this);
  }
}

}
