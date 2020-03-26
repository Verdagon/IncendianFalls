using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HoldPositionImpulseIncarnation : IHoldPositionImpulseEffectVisitor {
  public readonly int weight;
  public readonly int duration;
  public HoldPositionImpulseIncarnation(
      int weight,
      int duration) {
    this.weight = weight;
    this.duration = duration;
  }
  public HoldPositionImpulseIncarnation Copy() {
    return new HoldPositionImpulseIncarnation(
weight,
duration    );
  }

  public void visitHoldPositionImpulseCreateEffect(HoldPositionImpulseCreateEffect e) {}
  public void visitHoldPositionImpulseDeleteEffect(HoldPositionImpulseDeleteEffect e) {}


  public void ApplyEffect(IHoldPositionImpulseEffect effect) { effect.visitIHoldPositionImpulseEffect(this); }
}

}
