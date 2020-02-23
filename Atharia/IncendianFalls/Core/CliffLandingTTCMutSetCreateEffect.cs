using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffLandingTTCMutSetCreateEffect : ICliffLandingTTCMutSetEffect {
  public readonly int id;
  public CliffLandingTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICliffLandingTTCMutSetEffect.id => id;
  public void visit(ICliffLandingTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetCreateEffect(this);
  }
}

}
