using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavaArcanaLevelLinkerTTCIncarnation : IRavaArcanaLevelLinkerTTCEffectVisitor {
  public readonly int nextLevelDepth;
  public RavaArcanaLevelLinkerTTCIncarnation(
      int nextLevelDepth) {
    this.nextLevelDepth = nextLevelDepth;
  }
  public RavaArcanaLevelLinkerTTCIncarnation Copy() {
    return new RavaArcanaLevelLinkerTTCIncarnation(
nextLevelDepth    );
  }

  public void visitRavaArcanaLevelLinkerTTCCreateEffect(RavaArcanaLevelLinkerTTCCreateEffect e) {}
  public void visitRavaArcanaLevelLinkerTTCDeleteEffect(RavaArcanaLevelLinkerTTCDeleteEffect e) {}

  public void ApplyEffect(IRavaArcanaLevelLinkerTTCEffect effect) { effect.visitIRavaArcanaLevelLinkerTTCEffect(this); }
}

}
