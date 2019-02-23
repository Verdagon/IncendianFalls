using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCMutSetDeleteEffect : IShieldingUCMutSetEffect {
  public readonly int id;
  public ShieldingUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IShieldingUCMutSetEffect.id => id;
  public void visit(IShieldingUCMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCMutSetDeleteEffect(this);
  }
}

}
