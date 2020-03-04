using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WaterTTCDeleteEffect : IWaterTTCEffect {
  public readonly int id;
  public WaterTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IWaterTTCEffect.id => id;
  public void visit(IWaterTTCEffectVisitor visitor) {
    visitor.visitWaterTTCDeleteEffect(this);
  }
}

}
