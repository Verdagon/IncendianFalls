using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct GlaiveStrongMutSetCreateEffect : IGlaiveStrongMutSetEffect {
  public readonly int id;
  public GlaiveStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IGlaiveStrongMutSetEffect.id => id;
  public void visitIGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGlaiveStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
