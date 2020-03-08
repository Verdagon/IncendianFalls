using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyImpulseStrongMutSetAddEffect : IDefyImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public DefyImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IDefyImpulseStrongMutSetEffect.id => id;
  public void visit(IDefyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetAddEffect(this);
  }
}

}
