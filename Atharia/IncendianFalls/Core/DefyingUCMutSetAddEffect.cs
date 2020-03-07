using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyingUCMutSetAddEffect : IDefyingUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DefyingUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDefyingUCMutSetEffect.id => id;
  public void visit(IDefyingUCMutSetEffectVisitor visitor) {
    visitor.visitDefyingUCMutSetAddEffect(this);
  }
}

}
