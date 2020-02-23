using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListCreateEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public IUnitEventMutListCreateEffect(int id) {
    this.id = id;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visit(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListCreateEffect(this);
  }
}

}
