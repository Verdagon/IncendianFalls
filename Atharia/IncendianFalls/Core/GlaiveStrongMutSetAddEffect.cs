using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveStrongMutSetAddEffect : IGlaiveStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public GlaiveStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IGlaiveStrongMutSetEffect.id => id;
  public void visit(IGlaiveStrongMutSetEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetAddEffect(this);
  }
}

}
