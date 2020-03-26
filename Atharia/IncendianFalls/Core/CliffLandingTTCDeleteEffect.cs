using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffLandingTTCDeleteEffect : ICliffLandingTTCEffect {
  public readonly int id;
  public CliffLandingTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ICliffLandingTTCEffect.id => id;
  public void visitICliffLandingTTCEffect(ICliffLandingTTCEffectVisitor visitor) {
    visitor.visitCliffLandingTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffLandingTTCEffect(this);
  }
}

}
