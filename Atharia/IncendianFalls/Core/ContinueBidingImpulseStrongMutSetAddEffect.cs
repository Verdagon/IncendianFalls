using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ContinueBidingImpulseStrongMutSetAddEffect : IContinueBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ContinueBidingImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IContinueBidingImpulseStrongMutSetEffect.id => id;
  public void visit(IContinueBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetAddEffect(this);
  }
}

}
