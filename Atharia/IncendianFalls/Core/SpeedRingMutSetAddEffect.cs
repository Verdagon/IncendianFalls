using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingMutSetAddEffect : ISpeedRingMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SpeedRingMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISpeedRingMutSetEffect.id => id;
  public void visitISpeedRingMutSetEffect(ISpeedRingMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetEffect(this);
  }
}

}
