using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CounterImpulseCreateEffect : ICounterImpulseEffect {
  public readonly int id;
  public readonly CounterImpulseIncarnation incarnation;
  public CounterImpulseCreateEffect(int id, CounterImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICounterImpulseEffect.id => id;
  public void visitICounterImpulseEffect(ICounterImpulseEffectVisitor visitor) {
    visitor.visitCounterImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounterImpulseEffect(this);
  }
}

}
