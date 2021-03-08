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
  public void visitIWaterTTCEffect(IWaterTTCEffectVisitor visitor) {
    visitor.visitWaterTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWaterTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
