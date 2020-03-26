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
  public void visitISummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetEffect(this);
  }
}

}
