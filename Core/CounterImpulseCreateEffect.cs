using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CounterImpulseCreateEffect : ICounterImpulseEffect {
  public readonly int id;
  public CounterImpulseCreateEffect(int id) {
    this.id = id;
  }
  int ICounterImpulseEffect.id => id;
  public void visit(ICounterImpulseEffectVisitor visitor) {
    visitor.visitCounterImpulseCreateEffect(this);
  }
}

}
