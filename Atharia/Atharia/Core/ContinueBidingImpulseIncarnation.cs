using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ContinueBidingImpulseIncarnation : IContinueBidingImpulseEffectVisitor {
  public readonly int weight;
  public ContinueBidingImpulseIncarnation(
      int weight) {
    this.weight = weight;
  }
  public ContinueBidingImpulseIncarnation Copy() {
    return new ContinueBidingImpulseIncarnation(
weight    );
  }

  public void visitContinueBidingImpulseCreateEffect(ContinueBidingImpulseCreateEffect e) {}
  public void visitContinueBidingImpulseDeleteEffect(ContinueBidingImpulseDeleteEffect e) {}

  public void ApplyEffect(IContinueBidingImpulseEffect effect) { effect.visitIContinueBidingImpulseEffect(this); }
}

}
