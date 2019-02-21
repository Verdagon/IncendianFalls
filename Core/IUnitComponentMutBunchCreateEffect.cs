using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitComponentMutBunchCreateEffect : IIUnitComponentMutBunchEffect {
  public readonly int id;
  public readonly IUnitComponentMutBunchIncarnation incarnation;
  public IUnitComponentMutBunchCreateEffect(
      int id,
      IUnitComponentMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIUnitComponentMutBunchEffect.id => id;
  public void visit(IIUnitComponentMutBunchEffectVisitor visitor) {
    visitor.visitIUnitComponentMutBunchCreateEffect(this);
  }
}
       
}
