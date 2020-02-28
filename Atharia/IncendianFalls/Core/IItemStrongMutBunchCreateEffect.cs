using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemStrongMutBunchCreateEffect : IIItemStrongMutBunchEffect {
  public readonly int id;
  public IItemStrongMutBunchCreateEffect(int id) {
    this.id = id;
  }
  int IIItemStrongMutBunchEffect.id => id;
  public void visit(IIItemStrongMutBunchEffectVisitor visitor) {
    visitor.visitIItemStrongMutBunchCreateEffect(this);
  }
}

}
