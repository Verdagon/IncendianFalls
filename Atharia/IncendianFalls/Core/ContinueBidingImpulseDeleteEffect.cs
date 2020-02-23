using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ContinueBidingImpulseDeleteEffect : IContinueBidingImpulseEffect {
  public readonly int id;
  public ContinueBidingImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IContinueBidingImpulseEffect.id => id;
  public void visit(IContinueBidingImpulseEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseDeleteEffect(this);
  }
}

}
