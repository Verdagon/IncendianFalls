using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorMutSetRemoveEffect : IArmorMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ArmorMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IArmorMutSetEffect.id => id;
  public void visitIArmorMutSetEffect(IArmorMutSetEffectVisitor visitor) {
    visitor.visitArmorMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
