using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaterTTCIncarnation : IWaterTTCEffectVisitor {
  public WaterTTCIncarnation(
) {
  }
  public WaterTTCIncarnation Copy() {
    return new WaterTTCIncarnation(
    );
  }

  public void visitWaterTTCCreateEffect(WaterTTCCreateEffect e) {}
  public void visitWaterTTCDeleteEffect(WaterTTCDeleteEffect e) {}

  public void ApplyEffect(IWaterTTCEffect effect) { effect.visitIWaterTTCEffect(this); }
}

}
