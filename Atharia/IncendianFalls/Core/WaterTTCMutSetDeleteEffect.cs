using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WaterTTCMutSetDeleteEffect : IWaterTTCMutSetEffect {
  public readonly int id;
  public WaterTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IWaterTTCMutSetEffect.id => id;
  public void visit(IWaterTTCMutSetEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetDeleteEffect(this);
  }
}

}
