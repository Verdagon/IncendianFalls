using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorStrongMutSetAddEffect : IArmorStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ArmorStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IArmorStrongMutSetEffect.id => id;
  public void visitIArmorStrongMutSetEffect(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetEffect(this);
  }
}

}
