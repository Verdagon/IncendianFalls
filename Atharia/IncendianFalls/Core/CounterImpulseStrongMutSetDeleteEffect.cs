using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounterImpulseStrongMutSetDeleteEffect : ICounterImpulseStrongMutSetEffect {
  public readonly int id;
  public CounterImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICounterImpulseStrongMutSetEffect.id => id;
  public void visitICounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounterImpulseStrongMutSetEffect(this);
  }
}

}
