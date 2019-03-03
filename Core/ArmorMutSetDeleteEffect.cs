using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorMutSetDeleteEffect : IArmorMutSetEffect {
  public readonly int id;
  public ArmorMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IArmorMutSetEffect.id => id;
  public void visit(IArmorMutSetEffectVisitor visitor) {
    visitor.visitArmorMutSetDeleteEffect(this);
  }
}

}
