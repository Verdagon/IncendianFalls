using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct WaterTTCMutSetAddEffect : IWaterTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public WaterTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IWaterTTCMutSetEffect.id => id;
  public void visitIWaterTTCMutSetEffect(IWaterTTCMutSetEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWaterTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
