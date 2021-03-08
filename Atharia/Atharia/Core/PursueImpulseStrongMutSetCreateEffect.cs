using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct PursueImpulseStrongMutSetCreateEffect : IPursueImpulseStrongMutSetEffect {
  public readonly int id;
  public PursueImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IPursueImpulseStrongMutSetEffect.id => id;
  public void visitIPursueImpulseStrongMutSetEffect(IPursueImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitPursueImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitPursueImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
