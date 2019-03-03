using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemMutBunchCreateEffect : IIItemMutBunchEffect {
  public readonly int id;
  public IItemMutBunchCreateEffect(int id) {
    this.id = id;
  }
  int IIItemMutBunchEffect.id => id;
  public void visit(IIItemMutBunchEffectVisitor visitor) {
    visitor.visitIItemMutBunchCreateEffect(this);
  }
}

}
