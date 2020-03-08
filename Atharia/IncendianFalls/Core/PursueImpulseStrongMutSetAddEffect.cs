using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct PursueImpulseStrongMutSetAddEffect : IPursueImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public PursueImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IPursueImpulseStrongMutSetEffect.id => id;
  public void visit(IPursueImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitPursueImpulseStrongMutSetAddEffect(this);
  }
}

}
