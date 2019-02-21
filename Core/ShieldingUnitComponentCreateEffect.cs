using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ShieldingUnitComponentCreateEffect : IShieldingUnitComponentEffect {
  public readonly int id;
  public readonly ShieldingUnitComponentIncarnation incarnation;
  public ShieldingUnitComponentCreateEffect(
      int id,
      ShieldingUnitComponentIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IShieldingUnitComponentEffect.id => id;
  public void visit(IShieldingUnitComponentEffectVisitor visitor) {
    visitor.visitShieldingUnitComponentCreateEffect(this);
  }
}
       
}
