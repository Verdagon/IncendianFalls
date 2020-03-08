using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounterImpulseStrongMutSetAddEffect : ICounterImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CounterImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICounterImpulseStrongMutSetEffect.id => id;
  public void visit(ICounterImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetAddEffect(this);
  }
}

}
