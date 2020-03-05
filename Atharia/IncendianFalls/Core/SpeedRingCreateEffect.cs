using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SpeedRingCreateEffect : ISpeedRingEffect {
  public readonly int id;
  public SpeedRingCreateEffect(int id) {
    this.id = id;
  }
  int ISpeedRingEffect.id => id;
  public void visit(ISpeedRingEffectVisitor visitor) {
    visitor.visitSpeedRingCreateEffect(this);
  }
}

}
