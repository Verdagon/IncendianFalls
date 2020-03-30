using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingStrongMutSetDeleteEffect : ISpeedRingStrongMutSetEffect {
  public readonly int id;
  public SpeedRingStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISpeedRingStrongMutSetEffect.id => id;
  public void visitISpeedRingStrongMutSetEffect(ISpeedRingStrongMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
