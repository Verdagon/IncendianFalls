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
  public void visitIArmorStrongMutSetEffect(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
