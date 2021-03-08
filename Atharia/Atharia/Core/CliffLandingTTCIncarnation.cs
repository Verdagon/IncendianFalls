using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffLandingTTCIncarnation : ICliffLandingTTCEffectVisitor {
  public CliffLandingTTCIncarnation(
) {
  }
  public CliffLandingTTCIncarnation Copy() {
    return new CliffLandingTTCIncarnation(
    );
  }

  public void visitCliffLandingTTCCreateEffect(CliffLandingTTCCreateEffect e) {}
  public void visitCliffLandingTTCDeleteEffect(CliffLandingTTCDeleteEffect e) {}

  public void ApplyEffect(ICliffLandingTTCEffect effect) { effect.visitICliffLandingTTCEffect(this); }
}

}
