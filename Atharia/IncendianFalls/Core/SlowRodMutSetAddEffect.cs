using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodMutSetAddEffect : ISlowRodMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SlowRodMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISlowRodMutSetEffect.id => id;
  public void visit(ISlowRodMutSetEffectVisitor visitor) {
    visitor.visitSlowRodMutSetAddEffect(this);
  }
}

}
