using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WaterTTCCreateEffect : IWaterTTCEffect {
  public readonly int id;
  public WaterTTCCreateEffect(int id) {
    this.id = id;
  }
  int IWaterTTCEffect.id => id;
  public void visit(IWaterTTCEffectVisitor visitor) {
    visitor.visitWaterTTCCreateEffect(this);
  }
}

}
