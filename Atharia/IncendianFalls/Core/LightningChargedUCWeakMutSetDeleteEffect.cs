using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCWeakMutSetDeleteEffect : ILightningChargedUCWeakMutSetEffect {
  public readonly int id;
  public LightningChargedUCWeakMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ILightningChargedUCWeakMutSetEffect.id => id;
  public void visit(ILightningChargedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetDeleteEffect(this);
  }
}

}
