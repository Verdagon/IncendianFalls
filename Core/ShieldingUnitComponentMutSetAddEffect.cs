using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUnitComponentMutSetAddEffect : IShieldingUnitComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ShieldingUnitComponentMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IShieldingUnitComponentMutSetEffect.id => id;
  public void visit(IShieldingUnitComponentMutSetEffectVisitor visitor) {
    visitor.visitShieldingUnitComponentMutSetAddEffect(this);
  }
}

}
