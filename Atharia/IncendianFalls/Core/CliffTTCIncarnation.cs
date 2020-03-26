using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffTTCIncarnation : ICliffTTCEffectVisitor {
  public CliffTTCIncarnation(
) {
  }
  public CliffTTCIncarnation Copy() {
    return new CliffTTCIncarnation(
    );
  }

  public void visitCliffTTCCreateEffect(CliffTTCCreateEffect e) {}
  public void visitCliffTTCDeleteEffect(CliffTTCDeleteEffect e) {}

  public void ApplyEffect(ICliffTTCEffect effect) { effect.visitICliffTTCEffect(this); }
}

}
