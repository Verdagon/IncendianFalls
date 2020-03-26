using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargingUCMutSetDeleteEffect : ILightningChargingUCMutSetEffect {
  public readonly int id;
  public LightningChargingUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ILightningChargingUCMutSetEffect.id => id;
  public void visitILightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetEffect(this);
  }
}

}
