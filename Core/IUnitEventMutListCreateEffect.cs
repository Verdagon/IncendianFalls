using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListCreateEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public readonly IUnitEventMutListIncarnation incarnation;
  public IUnitEventMutListCreateEffect(
      int id,
      IUnitEventMutListIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visit(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListCreateEffect(this);
  }
}

}
