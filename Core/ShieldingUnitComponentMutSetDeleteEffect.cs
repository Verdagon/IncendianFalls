using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUnitComponentMutSetDeleteEffect : IShieldingUnitComponentMutSetEffect {
  public readonly int id;
  public ShieldingUnitComponentMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IShieldingUnitComponentMutSetEffect.id => id;
  public void visit(IShieldingUnitComponentMutSetEffectVisitor visitor) {
    visitor.visitShieldingUnitComponentMutSetDeleteEffect(this);
  }
}

}
