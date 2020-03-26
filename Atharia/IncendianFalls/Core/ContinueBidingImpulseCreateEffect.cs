using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ContinueBidingImpulseCreateEffect : IContinueBidingImpulseEffect {
  public readonly int id;
  public readonly ContinueBidingImpulseIncarnation incarnation;
  public ContinueBidingImpulseCreateEffect(int id, ContinueBidingImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IContinueBidingImpulseEffect.id => id;
  public void visitIContinueBidingImpulseEffect(IContinueBidingImpulseEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitContinueBidingImpulseEffect(this);
  }
}

}
