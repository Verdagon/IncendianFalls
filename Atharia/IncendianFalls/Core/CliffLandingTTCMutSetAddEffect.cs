using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffLandingTTCMutSetAddEffect : ICliffLandingTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CliffLandingTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICliffLandingTTCMutSetEffect.id => id;
  public void visitICliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
