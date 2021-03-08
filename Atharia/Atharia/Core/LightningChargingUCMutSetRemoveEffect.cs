using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargingUCMutSetRemoveEffect : ILightningChargingUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LightningChargingUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILightningChargingUCMutSetEffect.id => id;
  public void visitILightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
