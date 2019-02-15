using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitMutBunchCreateEffect : IUnitMutBunchEffect {
  public readonly int id;
  public readonly UnitMutBunchIncarnation incarnation;
  public UnitMutBunchCreateEffect(
      int id,
      UnitMutBunchIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUnitMutBunchEffect.id => id;
  public void visit(IUnitMutBunchEffectVisitor visitor) {
    visitor.visitUnitMutBunchCreateEffect(this);
  }
}

}
