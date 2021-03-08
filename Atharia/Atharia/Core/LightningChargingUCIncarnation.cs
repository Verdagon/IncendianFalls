using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargingUCIncarnation : ILightningChargingUCEffectVisitor {
  public LightningChargingUCIncarnation(
) {
  }
  public LightningChargingUCIncarnation Copy() {
    return new LightningChargingUCIncarnation(
    );
  }

  public void visitLightningChargingUCCreateEffect(LightningChargingUCCreateEffect e) {}
  public void visitLightningChargingUCDeleteEffect(LightningChargingUCDeleteEffect e) {}

  public void ApplyEffect(ILightningChargingUCEffect effect) { effect.visitILightningChargingUCEffect(this); }
}

}
