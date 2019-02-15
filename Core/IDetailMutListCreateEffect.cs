using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IDetailMutListCreateEffect : IIDetailMutListEffect {
  public readonly int id;
  public readonly IDetailMutListIncarnation incarnation;
  public IDetailMutListCreateEffect(
      int id,
      IDetailMutListIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIDetailMutListEffect.id => id;
  public void visit(IIDetailMutListEffectVisitor visitor) {
    visitor.visitIDetailMutListCreateEffect(this);
  }
}

}
