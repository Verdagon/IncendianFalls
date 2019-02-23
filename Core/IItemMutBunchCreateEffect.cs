using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IItemMutBunchCreateEffect : IIItemMutBunchEffect {
  public readonly int id;
  public readonly IItemMutBunchIncarnation incarnation;
  public IItemMutBunchCreateEffect(
      int id,
      IItemMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIItemMutBunchEffect.id => id;
  public void visit(IIItemMutBunchEffectVisitor visitor) {
    visitor.visitIItemMutBunchCreateEffect(this);
  }
}
       
}
