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
  public void visit(ISpeedRingStrongMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetDeleteEffect(this);
  }
}

}
