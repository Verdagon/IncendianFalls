using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingStrongMutSetCreateEffect : ISpeedRingStrongMutSetEffect {
  public readonly int id;
  public SpeedRingStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISpeedRingStrongMutSetEffect.id => id;
  public void visitISpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetEffect(this);
  }
}

}
