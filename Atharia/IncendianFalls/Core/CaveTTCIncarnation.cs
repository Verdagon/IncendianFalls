using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveTTCIncarnation : ICaveTTCEffectVisitor {
  public CaveTTCIncarnation(
) {
  }
  public CaveTTCIncarnation Copy() {
    return new CaveTTCIncarnation(
    );
  }

  public void visitCaveTTCCreateEffect(CaveTTCCreateEffect e) {}
  public void visitCaveTTCDeleteEffect(CaveTTCDeleteEffect e) {}

  public void ApplyEffect(ICaveTTCEffect effect) { effect.visitICaveTTCEffect(this); }
}

}
