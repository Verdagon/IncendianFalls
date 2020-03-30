using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorMutSetCreateEffect : IArmorMutSetEffect {
  public readonly int id;
  public ArmorMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IArmorMutSetEffect.id => id;
  public void visitIArmorMutSetEffect(IArmorMutSetEffectVisitor visitor) {
    visitor.visitArmorMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
