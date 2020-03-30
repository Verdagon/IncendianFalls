using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorStrongMutSetCreateEffect : IArmorStrongMutSetEffect {
  public readonly int id;
  public ArmorStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IArmorStrongMutSetEffect.id => id;
  public void visitIArmorStrongMutSetEffect(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
