using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IPreActingUnitComponentMutBunchCreateEffect : IIPreActingUnitComponentMutBunchEffect {
  public readonly int id;
  public readonly IPreActingUnitComponentMutBunchIncarnation incarnation;
  public IPreActingUnitComponentMutBunchCreateEffect(
      int id,
      IPreActingUnitComponentMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIPreActingUnitComponentMutBunchEffect.id => id;
  public void visit(IIPreActingUnitComponentMutBunchEffectVisitor visitor) {
    visitor.visitIPreActingUnitComponentMutBunchCreateEffect(this);
  }
}
       
}
