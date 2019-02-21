using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUnitComponentMutBunchCreateEffect : IIPostActingUnitComponentMutBunchEffect {
  public readonly int id;
  public readonly IPostActingUnitComponentMutBunchIncarnation incarnation;
  public IPostActingUnitComponentMutBunchCreateEffect(
      int id,
      IPostActingUnitComponentMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIPostActingUnitComponentMutBunchEffect.id => id;
  public void visit(IIPostActingUnitComponentMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUnitComponentMutBunchCreateEffect(this);
  }
}
       
}
