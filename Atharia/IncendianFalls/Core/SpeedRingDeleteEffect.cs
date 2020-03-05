using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SpeedRingDeleteEffect : ISpeedRingEffect {
  public readonly int id;
  public SpeedRingDeleteEffect(int id) {
    this.id = id;
  }
  int ISpeedRingEffect.id => id;
  public void visit(ISpeedRingEffectVisitor visitor) {
    visitor.visitSpeedRingDeleteEffect(this);
  }
}

}
