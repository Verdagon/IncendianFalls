using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCWeakMutSetRemoveEffect : IDefyingUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DefyingUCWeakMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDefyingUCWeakMutSetEffect.id => id;
  public void visit(IDefyingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetRemoveEffect(this);
  }
}

}
