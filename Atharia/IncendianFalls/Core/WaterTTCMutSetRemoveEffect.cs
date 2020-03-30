using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WaterTTCMutSetRemoveEffect : IWaterTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WaterTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWaterTTCMutSetEffect.id => id;
  public void visitIWaterTTCMutSetEffect(IWaterTTCMutSetEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
