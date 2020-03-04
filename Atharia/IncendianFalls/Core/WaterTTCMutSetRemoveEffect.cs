using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WaterTTCMutSetRemoveEffect : IWaterTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WaterTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWaterTTCMutSetEffect.id => id;
  public void visit(IWaterTTCMutSetEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetRemoveEffect(this);
  }
}

}
