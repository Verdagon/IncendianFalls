using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonImpulseStrongMutSetRemoveEffect : ISummonImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SummonImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISummonImpulseStrongMutSetEffect.id => id;
  public void visitISummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetEffect(this);
  }
}

}
