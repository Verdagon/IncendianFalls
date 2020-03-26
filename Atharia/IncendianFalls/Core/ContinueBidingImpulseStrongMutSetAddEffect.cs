using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ContinueBidingImpulseStrongMutSetAddEffect : IContinueBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ContinueBidingImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IContinueBidingImpulseStrongMutSetEffect.id => id;
  public void visitIContinueBidingImpulseStrongMutSetEffect(IContinueBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetEffect(this);
  }
}

}
