using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffLandingTTCMutSetRemoveEffect : ICliffLandingTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CliffLandingTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICliffLandingTTCMutSetEffect.id => id;
  public void visitICliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetEffect(this);
  }
}

}
