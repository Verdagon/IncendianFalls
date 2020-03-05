using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingStrongMutSetRemoveEffect : ISpeedRingStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SpeedRingStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISpeedRingStrongMutSetEffect.id => id;
  public void visit(ISpeedRingStrongMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetRemoveEffect(this);
  }
}

}
