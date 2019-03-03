using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitComponentMutBunchCreateEffect : IIUnitComponentMutBunchEffect {
  public readonly int id;
  public IUnitComponentMutBunchCreateEffect(int id) {
    this.id = id;
  }
  int IIUnitComponentMutBunchEffect.id => id;
  public void visit(IIUnitComponentMutBunchEffectVisitor visitor) {
    visitor.visitIUnitComponentMutBunchCreateEffect(this);
  }
}

}
