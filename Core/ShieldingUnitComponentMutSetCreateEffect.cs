using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUnitComponentMutSetCreateEffect : IShieldingUnitComponentMutSetEffect {
  public readonly int id;
  public readonly ShieldingUnitComponentMutSetIncarnation incarnation;
  public ShieldingUnitComponentMutSetCreateEffect(
      int id,
      ShieldingUnitComponentMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IShieldingUnitComponentMutSetEffect.id => id;
  public void visit(IShieldingUnitComponentMutSetEffectVisitor visitor) {
    visitor.visitShieldingUnitComponentMutSetCreateEffect(this);
  }
}

}
