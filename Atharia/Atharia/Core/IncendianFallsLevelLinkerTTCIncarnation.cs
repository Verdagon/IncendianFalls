using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class IncendianFallsLevelLinkerTTCIncarnation : IIncendianFallsLevelLinkerTTCEffectVisitor {
  public readonly int thisLevelDepth;
  public IncendianFallsLevelLinkerTTCIncarnation(
      int thisLevelDepth) {
    this.thisLevelDepth = thisLevelDepth;
  }
  public IncendianFallsLevelLinkerTTCIncarnation Copy() {
    return new IncendianFallsLevelLinkerTTCIncarnation(
thisLevelDepth    );
  }

  public void visitIncendianFallsLevelLinkerTTCCreateEffect(IncendianFallsLevelLinkerTTCCreateEffect e) {}
  public void visitIncendianFallsLevelLinkerTTCDeleteEffect(IncendianFallsLevelLinkerTTCDeleteEffect e) {}

  public void ApplyEffect(IIncendianFallsLevelLinkerTTCEffect effect) { effect.visitIIncendianFallsLevelLinkerTTCEffect(this); }
}

}
