using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCWeakMutSetAddEffect : IDefyingUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DefyingUCWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDefyingUCWeakMutSetEffect.id => id;
  public void visit(IDefyingUCWeakMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCWeakMutSetAddEffect(this);
  }
}

}
