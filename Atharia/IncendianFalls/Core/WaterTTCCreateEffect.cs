using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WaterTTCCreateEffect : IWaterTTCEffect {
  public readonly int id;
  public readonly WaterTTCIncarnation incarnation;
  public WaterTTCCreateEffect(int id, WaterTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IWaterTTCEffect.id => id;
  public void visitIWaterTTCEffect(IWaterTTCEffectVisitor visitor) {
    visitor.visitWaterTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWaterTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
