using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingStrongMutSetAddEffect : ISpeedRingStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SpeedRingStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISpeedRingStrongMutSetEffect.id => id;
  public void visitISpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
