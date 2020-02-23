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
  public void visit(ICliffLandingTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffLandingTTCMutSetDeleteEffect(this);
  }
}

}
