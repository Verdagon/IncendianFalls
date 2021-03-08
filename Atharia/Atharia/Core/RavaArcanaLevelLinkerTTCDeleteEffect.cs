using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavaArcanaLevelLinkerTTCDeleteEffect : IRavaArcanaLevelLinkerTTCEffect {
  public readonly int id;
  public RavaArcanaLevelLinkerTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IRavaArcanaLevelLinkerTTCEffect.id => id;
  public void visitIRavaArcanaLevelLinkerTTCEffect(IRavaArcanaLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
