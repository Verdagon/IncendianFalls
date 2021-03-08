using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaArcanaLevelLinkerTTCMutSetDeleteEffect : IRavaArcanaLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public RavaArcanaLevelLinkerTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IRavaArcanaLevelLinkerTTCMutSetEffect.id => id;
  public void visitIRavaArcanaLevelLinkerTTCMutSetEffect(IRavaArcanaLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
