using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorStrongMutSetRemoveEffect : IArmorStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ArmorStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IArmorStrongMutSetEffect.id => id;
  public void visitIArmorStrongMutSetEffect(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetEffect(this);
  }
}

}
