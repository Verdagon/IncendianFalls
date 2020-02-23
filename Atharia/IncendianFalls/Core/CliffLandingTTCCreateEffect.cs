using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffLandingTTCCreateEffect : ICliffLandingTTCEffect {
  public readonly int id;
  public CliffLandingTTCCreateEffect(int id) {
    this.id = id;
  }
  int ICliffLandingTTCEffect.id => id;
  public void visit(ICliffLandingTTCEffectVisitor visitor) {
    visitor.visitCliffLandingTTCCreateEffect(this);
  }
}

}
