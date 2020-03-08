using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvaporateImpulseStrongMutSetAddEffect : IEvaporateImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public EvaporateImpulseStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IEvaporateImpulseStrongMutSetEffect.id => id;
  public void visit(IEvaporateImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetAddEffect(this);
  }
}

}
