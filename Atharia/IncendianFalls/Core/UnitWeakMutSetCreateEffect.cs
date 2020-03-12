using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitWeakMutSetCreateEffect : IUnitWeakMutSetEffect {
  public readonly int id;
  public UnitWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IUnitWeakMutSetEffect.id => id;
  public void visit(IUnitWeakMutSetEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetCreateEffect(this);
  }
}

}
