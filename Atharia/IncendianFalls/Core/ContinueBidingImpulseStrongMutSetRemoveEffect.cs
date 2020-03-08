using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ContinueBidingImpulseStrongMutSetRemoveEffect : IContinueBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public ContinueBidingImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IContinueBidingImpulseStrongMutSetEffect.id => id;
  public void visit(IContinueBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetRemoveEffect(this);
  }
}

}
