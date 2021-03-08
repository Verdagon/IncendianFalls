using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IImpulseStrongMutBunchCreateEffect : IIImpulseStrongMutBunchEffect {
  public readonly int id;
  public readonly IImpulseStrongMutBunchIncarnation incarnation;
  public IImpulseStrongMutBunchCreateEffect(int id, IImpulseStrongMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIImpulseStrongMutBunchEffect.id => id;
  public void visitIIImpulseStrongMutBunchEffect(IIImpulseStrongMutBunchEffectVisitor visitor) {
    visitor.visitIImpulseStrongMutBunchCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIImpulseStrongMutBunchEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
