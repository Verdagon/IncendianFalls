using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ContinueBidingImpulseStrongMutSetCreateEffect : IContinueBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public ContinueBidingImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IContinueBidingImpulseStrongMutSetEffect.id => id;
  public void visit(IContinueBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetCreateEffect(this);
  }
}

}