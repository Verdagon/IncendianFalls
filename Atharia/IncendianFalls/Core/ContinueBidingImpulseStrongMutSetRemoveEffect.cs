using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ContinueBidingImpulseStrongMutSetRemoveEffect : IContinueBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ContinueBidingImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IContinueBidingImpulseStrongMutSetEffect.id => id;
  public void visitIContinueBidingImpulseStrongMutSetEffect(IContinueBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
