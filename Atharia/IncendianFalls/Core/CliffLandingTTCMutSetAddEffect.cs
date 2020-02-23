using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffLandingTTCMutSetAddEffect : ICliffLandingTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CliffLandingTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICliffLandingTTCMutSetEffect.id => id;
  public void visit(ICliffLandingTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetAddEffect(this);
  }
}

}
