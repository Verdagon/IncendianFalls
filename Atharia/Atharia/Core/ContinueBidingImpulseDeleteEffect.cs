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
  public void visitIContinueBidingImpulseEffect(IContinueBidingImpulseEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
