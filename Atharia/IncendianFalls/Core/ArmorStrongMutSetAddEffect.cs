using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorStrongMutSetAddEffect : IArmorStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ArmorStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IArmorStrongMutSetEffect.id => id;
  public void visit(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetAddEffect(this);
  }
}

}
