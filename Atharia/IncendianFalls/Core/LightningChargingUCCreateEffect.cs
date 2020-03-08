using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LightningChargingUCCreateEffect : ILightningChargingUCEffect {
  public readonly int id;
  public LightningChargingUCCreateEffect(int id) {
    this.id = id;
  }
  int ILightningChargingUCEffect.id => id;
  public void visit(ILightningChargingUCEffectVisitor visitor) {
    visitor.visitLightningChargingUCCreateEffect(this);
  }
}

}
