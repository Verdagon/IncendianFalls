using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounterImpulseStrongMutSetCreateEffect : ICounterImpulseStrongMutSetEffect {
  public readonly int id;
  public CounterImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICounterImpulseStrongMutSetEffect.id => id;
  public void visit(ICounterImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetCreateEffect(this);
  }
}

}
