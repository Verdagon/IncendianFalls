using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounterImpulseStrongMutSetAddEffect : ICounterImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CounterImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICounterImpulseStrongMutSetEffect.id => id;
  public void visitICounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetEffect(this);
  }
}

}
