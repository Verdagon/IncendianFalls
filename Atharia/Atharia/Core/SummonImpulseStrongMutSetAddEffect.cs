using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonImpulseStrongMutSetAddEffect : ISummonImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public SummonImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ISummonImpulseStrongMutSetEffect.id => id;
  public void visitISummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
