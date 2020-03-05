using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingMutSetRemoveEffect : ISpeedRingMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SpeedRingMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISpeedRingMutSetEffect.id => id;
  public void visit(ISpeedRingMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingMutSetRemoveEffect(this);
  }
}

}
