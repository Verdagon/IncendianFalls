using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffLandingTTCMutSetDeleteEffect : ICliffLandingTTCMutSetEffect {
  public readonly int id;
  public CliffLandingTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICliffLandingTTCMutSetEffect.id => id;
  public void visitICliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
