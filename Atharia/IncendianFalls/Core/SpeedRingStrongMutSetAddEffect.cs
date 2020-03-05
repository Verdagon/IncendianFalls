using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SpeedRingStrongMutSetAddEffect : ISpeedRingStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SpeedRingStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISpeedRingStrongMutSetEffect.id => id;
  public void visit(ISpeedRingStrongMutSetEffectVisitor visitor) {
    visitor.visitSpeedRingStrongMutSetAddEffect(this);
  }
}

}
