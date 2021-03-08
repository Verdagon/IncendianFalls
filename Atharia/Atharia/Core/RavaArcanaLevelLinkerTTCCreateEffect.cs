using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavaArcanaLevelLinkerTTCCreateEffect : IRavaArcanaLevelLinkerTTCEffect {
  public readonly int id;
  public readonly RavaArcanaLevelLinkerTTCIncarnation incarnation;
  public RavaArcanaLevelLinkerTTCCreateEffect(int id, RavaArcanaLevelLinkerTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IRavaArcanaLevelLinkerTTCEffect.id => id;
  public void visitIRavaArcanaLevelLinkerTTCEffect(IRavaArcanaLevelLinkerTTCEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaArcanaLevelLinkerTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
