using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct ArmorMutSetRemoveEffect : IArmorMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ArmorMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IArmorMutSetEffect.id => id;
  public void visit(IArmorMutSetEffectVisitor visitor) {
    visitor.visitArmorMutSetRemoveEffect(this);
  }
}

}
