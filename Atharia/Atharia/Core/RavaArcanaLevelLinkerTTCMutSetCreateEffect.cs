using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaArcanaLevelLinkerTTCMutSetCreateEffect : IRavaArcanaLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public RavaArcanaLevelLinkerTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IRavaArcanaLevelLinkerTTCMutSetEffect.id => id;
  public void visitIRavaArcanaLevelLinkerTTCMutSetEffect(IRavaArcanaLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
