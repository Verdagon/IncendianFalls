using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ArmorDeleteEffect : IArmorEffect {
  public readonly int id;
  public ArmorDeleteEffect(int id) {
    this.id = id;
  }
  int IArmorEffect.id => id;
  public void visitIArmorEffect(IArmorEffectVisitor visitor) {
    visitor.visitArmorDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
