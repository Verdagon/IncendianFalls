using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargingUCMutSetAddEffect : ILightningChargingUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LightningChargingUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILightningChargingUCMutSetEffect.id => id;
  public void visitILightningChargingUCMutSetEffect(ILightningChargingUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetEffect(this);
  }
}

}
