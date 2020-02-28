using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveStrongMutSetRemoveEffect : IGlaiveStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public GlaiveStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IGlaiveStrongMutSetEffect.id => id;
  public void visit(IGlaiveStrongMutSetEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetRemoveEffect(this);
  }
}

}
