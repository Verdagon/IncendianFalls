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
  public void visitIWaterTTCMutSetEffect(IWaterTTCMutSetEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetEffect(this);
  }
}

}
