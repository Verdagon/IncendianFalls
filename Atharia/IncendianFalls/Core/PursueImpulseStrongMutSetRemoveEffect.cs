using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct PursueImpulseStrongMutSetRemoveEffect : IPursueImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public PursueImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IPursueImpulseStrongMutSetEffect.id => id;
  public void visitIPursueImpulseStrongMutSetEffect(IPursueImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitPursueImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitPursueImpulseStrongMutSetEffect(this);
  }
}

}
