using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingMutSetCreateEffect : ISpeedRingMutSetEffect {
  public readonly int id;
  public SpeedRingMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISpeedRingMutSetEffect.id => id;
  public void visitISpeedRingMutSetEffect(ISpeedRingMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetEffect(this);
  }
}

}
