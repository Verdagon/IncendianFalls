using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SummonImpulseStrongMutSetAddEffect : ISummonImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SummonImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISummonImpulseStrongMutSetEffect.id => id;
  public void visit(ISummonImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitSummonImpulseStrongMutSetAddEffect(this);
  }
}

}
