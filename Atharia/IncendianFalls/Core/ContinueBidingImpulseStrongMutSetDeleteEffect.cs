using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ContinueBidingImpulseStrongMutSetDeleteEffect : IContinueBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public ContinueBidingImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IContinueBidingImpulseStrongMutSetEffect.id => id;
  public void visit(IContinueBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseStrongMutSetDeleteEffect(this);
  }
}

}
