using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ShieldingUCWeakMutSetDeleteEffect : IShieldingUCWeakMutSetEffect {
  public readonly int id;
  public ShieldingUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IShieldingUCWeakMutSetEffect.id => id;
  public void visit(IShieldingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitShieldingUCWeakMutSetDeleteEffect(this);
  }
}

}
