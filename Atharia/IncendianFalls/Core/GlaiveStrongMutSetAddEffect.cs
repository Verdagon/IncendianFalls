using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveStrongMutSetAddEffect : IGlaiveStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public GlaiveStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IGlaiveStrongMutSetEffect.id => id;
  public void visitIGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetEffect(this);
  }
}

}
