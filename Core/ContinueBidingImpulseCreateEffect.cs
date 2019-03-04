using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ContinueBidingImpulseCreateEffect : IContinueBidingImpulseEffect {
  public readonly int id;
  public ContinueBidingImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IContinueBidingImpulseEffect.id => id;
  public void visit(IContinueBidingImpulseEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseCreateEffect(this);
  }
}

}
