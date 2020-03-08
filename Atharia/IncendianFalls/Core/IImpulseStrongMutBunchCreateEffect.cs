using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IImpulseStrongMutBunchCreateEffect : IIImpulseStrongMutBunchEffect {
  public readonly int id;
  public IImpulseStrongMutBunchCreateEffect(int id) {
    this.id = id;
  }
  int IIImpulseStrongMutBunchEffect.id => id;
  public void visit(IIImpulseStrongMutBunchEffectVisitor visitor) {
    visitor.visitIImpulseStrongMutBunchCreateEffect(this);
  }
}

}
