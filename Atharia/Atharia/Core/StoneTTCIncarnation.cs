using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StoneTTCIncarnation : IStoneTTCEffectVisitor {
  public StoneTTCIncarnation(
) {
  }
  public StoneTTCIncarnation Copy() {
    return new StoneTTCIncarnation(
    );
  }

  public void visitStoneTTCCreateEffect(StoneTTCCreateEffect e) {}
  public void visitStoneTTCDeleteEffect(StoneTTCDeleteEffect e) {}

  public void ApplyEffect(IStoneTTCEffect effect) { effect.visitIStoneTTCEffect(this); }
}

}
