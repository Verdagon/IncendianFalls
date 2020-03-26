using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CounterImpulseDeleteEffect : ICounterImpulseEffect {
  public readonly int id;
  public CounterImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int ICounterImpulseEffect.id => id;
  public void visitICounterImpulseEffect(ICounterImpulseEffectVisitor visitor) {
    visitor.visitCounterImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounterImpulseEffect(this);
  }
}

}
