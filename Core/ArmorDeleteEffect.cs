using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ArmorDeleteEffect : IArmorEffect {
  public readonly int id;
  public ArmorDeleteEffect(int id) {
    this.id = id;
  }
  int IArmorEffect.id => id;
  public void visit(IArmorEffectVisitor visitor) {
    visitor.visitArmorDeleteEffect(this);
  }
}
       
}
