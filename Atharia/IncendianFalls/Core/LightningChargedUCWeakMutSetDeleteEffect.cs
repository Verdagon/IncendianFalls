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
  public void visitILightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetEffect(this);
  }
}

}
