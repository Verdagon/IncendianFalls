using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LightningChargingUCCreateEffect : ILightningChargingUCEffect {
  public readonly int id;
  public readonly LightningChargingUCIncarnation incarnation;
  public LightningChargingUCCreateEffect(int id, LightningChargingUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILightningChargingUCEffect.id => id;
  public void visitILightningChargingUCEffect(ILightningChargingUCEffectVisitor visitor) {
    visitor.visitLightningChargingUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargingUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
