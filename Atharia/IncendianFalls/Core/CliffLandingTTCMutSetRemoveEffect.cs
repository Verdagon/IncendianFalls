using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffLandingTTCMutSetRemoveEffect : ICliffLandingTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CliffLandingTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICliffLandingTTCMutSetEffect.id => id;
  public void visit(ICliffLandingTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetRemoveEffect(this);
  }
}

}
