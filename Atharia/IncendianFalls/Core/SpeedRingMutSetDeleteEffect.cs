using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingMutSetDeleteEffect : ISpeedRingMutSetEffect {
  public readonly int id;
  public SpeedRingMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISpeedRingMutSetEffect.id => id;
  public void visitISpeedRingMutSetEffect(ISpeedRingMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetEffect(this);
  }
}

}
