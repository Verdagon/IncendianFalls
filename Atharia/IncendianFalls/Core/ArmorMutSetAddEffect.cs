using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorMutSetAddEffect : IArmorMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ArmorMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IArmorMutSetEffect.id => id;
  public void visit(IArmorMutSetEffectVisitor visitor) {
    visitor.visitArmorMutSetAddEffect(this);
  }
}

}
