using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaArcanaLevelLinkerTTCMutSetAddEffect : IRavaArcanaLevelLinkerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RavaArcanaLevelLinkerTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRavaArcanaLevelLinkerTTCMutSetEffect.id => id;
  public void visitIRavaArcanaLevelLinkerTTCMutSetEffect(IRavaArcanaLevelLinkerTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
