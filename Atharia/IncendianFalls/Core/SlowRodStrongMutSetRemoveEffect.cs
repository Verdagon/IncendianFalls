using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodStrongMutSetRemoveEffect : ISlowRodStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SlowRodStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISlowRodStrongMutSetEffect.id => id;
  public void visit(ISlowRodStrongMutSetEffectVisitor visitor) {
    visitor.visitSlowRodStrongMutSetRemoveEffect(this);
  }
}

}
