using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class StartBidingImpulseIncarnation : IStartBidingImpulseEffectVisitor {
  public readonly int weight;
  public StartBidingImpulseIncarnation(
      int weight) {
    this.weight = weight;
  }
  public StartBidingImpulseIncarnation Copy() {
    return new StartBidingImpulseIncarnation(
weight    );
  }

  public void visitStartBidingImpulseCreateEffect(StartBidingImpulseCreateEffect e) {}
  public void visitStartBidingImpulseDeleteEffect(StartBidingImpulseDeleteEffect e) {}

  public void ApplyEffect(IStartBidingImpulseEffect effect) { effect.visitIStartBidingImpulseEffect(this); }
}

}
