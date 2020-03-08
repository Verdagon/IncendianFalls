using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodMutSetRemoveEffect : ISlowRodMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public SlowRodMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ISlowRodMutSetEffect.id => id;
  public void visit(ISlowRodMutSetEffectVisitor visitor) {
    visitor.visitSlowRodMutSetRemoveEffect(this);
  }
}

}
