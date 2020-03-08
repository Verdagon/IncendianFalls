using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargingUCMutSetRemoveEffect : ILightningChargingUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public LightningChargingUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILightningChargingUCMutSetEffect.id => id;
  public void visit(ILightningChargingUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargingUCMutSetRemoveEffect(this);
  }
}

}
