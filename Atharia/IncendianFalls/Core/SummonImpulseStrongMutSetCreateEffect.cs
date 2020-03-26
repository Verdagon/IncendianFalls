using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonImpulseStrongMutSetCreateEffect : ISummonImpulseStrongMutSetEffect {
  public readonly int id;
  public SummonImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISummonImpulseStrongMutSetEffect.id => id;
  public void visitISummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetEffect(this);
  }
}

}
