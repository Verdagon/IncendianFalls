using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BloodTTCIncarnation : IBloodTTCEffectVisitor {
  public BloodTTCIncarnation(
) {
  }
  public BloodTTCIncarnation Copy() {
    return new BloodTTCIncarnation(
    );
  }

  public void visitBloodTTCCreateEffect(BloodTTCCreateEffect e) {}
  public void visitBloodTTCDeleteEffect(BloodTTCDeleteEffect e) {}

  public void ApplyEffect(IBloodTTCEffect effect) { effect.visitIBloodTTCEffect(this); }
}

}
