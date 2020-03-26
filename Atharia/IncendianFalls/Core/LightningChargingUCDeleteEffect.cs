using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LightningChargingUCDeleteEffect : ILightningChargingUCEffect {
  public readonly int id;
  public LightningChargingUCDeleteEffect(int id) {
    this.id = id;
  }
  int ILightningChargingUCEffect.id => id;
  public void visitILightningChargingUCEffect(ILightningChargingUCEffectVisitor visitor) {
    visitor.visitLightningChargingUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargingUCEffect(this);
  }
}

}
