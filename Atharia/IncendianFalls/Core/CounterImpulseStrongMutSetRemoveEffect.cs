using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounterImpulseStrongMutSetRemoveEffect : ICounterImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CounterImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICounterImpulseStrongMutSetEffect.id => id;
  public void visitICounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
