using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffLandingTTCCreateEffect : ICliffLandingTTCEffect {
  public readonly int id;
  public readonly CliffLandingTTCIncarnation incarnation;
  public CliffLandingTTCCreateEffect(int id, CliffLandingTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICliffLandingTTCEffect.id => id;
  public void visitICliffLandingTTCEffect(ICliffLandingTTCEffectVisitor visitor) {
    visitor.visitCliffLandingTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffLandingTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
