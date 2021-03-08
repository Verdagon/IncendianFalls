using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaArcanaLevelLinkerTTCMutSetRemoveEffect : IRavaArcanaLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RavaArcanaLevelLinkerTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRavaArcanaLevelLinkerTTCMutSetEffect.id => id;
  public void visitIRavaArcanaLevelLinkerTTCMutSetEffect(IRavaArcanaLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
