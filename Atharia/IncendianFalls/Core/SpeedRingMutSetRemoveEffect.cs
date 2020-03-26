using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingMutSetRemoveEffect : ISpeedRingMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SpeedRingMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISpeedRingMutSetEffect.id => id;
  public void visitISpeedRingMutSetEffect(ISpeedRingMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetEffect(this);
  }
}

}
