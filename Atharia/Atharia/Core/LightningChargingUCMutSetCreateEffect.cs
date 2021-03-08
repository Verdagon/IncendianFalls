using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargingUCMutSetCreateEffect : ILightningChargingUCMutSetEffect {
  public readonly int id;
  public LightningChargingUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ILightningChargingUCMutSetEffect.id => id;
  public void visitILightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
