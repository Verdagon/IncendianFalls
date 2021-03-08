using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveStrongMutSetRemoveEffect : IGlaiveStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public GlaiveStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IGlaiveStrongMutSetEffect.id => id;
  public void visitIGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
