using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ShieldingUnitComponentSetPowerEffect : IShieldingUnitComponentEffect {
  public readonly int id;
  public readonly int newValue;
  public ShieldingUnitComponentSetPowerEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IShieldingUnitComponentEffect.id => id;

  public void visit(IShieldingUnitComponentEffectVisitor visitor) {
    visitor.visitShieldingUnitComponentSetPowerEffect(this);
  }
}
           
}
