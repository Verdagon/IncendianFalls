using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonImpulseStrongMutSetDeleteEffect : ISummonImpulseStrongMutSetEffect {
  public readonly int id;
  public SummonImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ISummonImpulseStrongMutSetEffect.id => id;
  public void visit(ISummonImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetDeleteEffect(this);
  }
}

}
