using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorStrongMutSetRemoveEffect : IArmorStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ArmorStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IArmorStrongMutSetEffect.id => id;
  public void visit(IArmorStrongMutSetEffectVisitor visitor) {
    visitor.visitArmorStrongMutSetRemoveEffect(this);
  }
}

}
