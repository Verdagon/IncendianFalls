using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounterImpulseIncarnation : ICounterImpulseEffectVisitor {
  public readonly int weight;
  public CounterImpulseIncarnation(
      int weight) {
    this.weight = weight;
  }
  public CounterImpulseIncarnation Copy() {
    return new CounterImpulseIncarnation(
weight    );
  }

  public void visitCounterImpulseCreateEffect(CounterImpulseCreateEffect e) {}
  public void visitCounterImpulseDeleteEffect(CounterImpulseDeleteEffect e) {}

  public void ApplyEffect(ICounterImpulseEffect effect) { effect.visitICounterImpulseEffect(this); }
}

}
