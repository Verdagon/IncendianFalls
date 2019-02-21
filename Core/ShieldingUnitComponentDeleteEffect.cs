using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct ShieldingUnitComponentDeleteEffect : IShieldingUnitComponentEffect {
  public readonly int id;
  public ShieldingUnitComponentDeleteEffect(int id) {
    this.id = id;
  }
  int IShieldingUnitComponentEffect.id => id;
  public void visit(IShieldingUnitComponentEffectVisitor visitor) {
    visitor.visitShieldingUnitComponentDeleteEffect(this);
  }
}
       
}
