using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ShieldingUCDeleteEffect : IShieldingUCEffect {
  public readonly int id;
  public ShieldingUCDeleteEffect(int id) {
    this.id = id;
  }
  int IShieldingUCEffect.id => id;
  public void visit(IShieldingUCEffectVisitor visitor) {
    visitor.visitShieldingUCDeleteEffect(this);
  }
}

}
