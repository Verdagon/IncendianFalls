using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingStrongMutSetRemoveEffect : ISpeedRingStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SpeedRingStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISpeedRingStrongMutSetEffect.id => id;
  public void visitISpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetEffect(this);
  }
}

}
