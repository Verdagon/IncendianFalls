using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodStrongMutSetAddEffect : ISlowRodStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SlowRodStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISlowRodStrongMutSetEffect.id => id;
  public void visit(ISlowRodStrongMutSetEffectVisitor visitor) {
    visitor.visitSlowRodStrongMutSetAddEffect(this);
  }
}

}
