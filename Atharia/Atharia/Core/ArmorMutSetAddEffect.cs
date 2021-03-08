using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorMutSetAddEffect : IArmorMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ArmorMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IArmorMutSetEffect.id => id;
  public void visitIArmorMutSetEffect(IArmorMutSetEffectVisitor visitor) {
    visitor.visitArmorMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
