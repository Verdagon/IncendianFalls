using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WaterTTCMutSetCreateEffect : IWaterTTCMutSetEffect {
  public readonly int id;
  public WaterTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IWaterTTCMutSetEffect.id => id;
  public void visit(IWaterTTCMutSetEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetCreateEffect(this);
  }
}

}
