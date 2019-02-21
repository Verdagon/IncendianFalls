using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUnitComponentMutSetRemoveEffect : IShieldingUnitComponentMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ShieldingUnitComponentMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IShieldingUnitComponentMutSetEffect.id => id;
  public void visit(IShieldingUnitComponentMutSetEffectVisitor visitor) {
    visitor.visitShieldingUnitComponentMutSetRemoveEffect(this);
  }
}

}
