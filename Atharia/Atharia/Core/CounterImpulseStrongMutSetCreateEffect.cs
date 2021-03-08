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
  public void visitICounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
