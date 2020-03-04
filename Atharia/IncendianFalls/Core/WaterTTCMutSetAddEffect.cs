using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WaterTTCMutSetAddEffect : IWaterTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public WaterTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IWaterTTCMutSetEffect.id => id;
  public void visit(IWaterTTCMutSetEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetAddEffect(this);
  }
}

}
