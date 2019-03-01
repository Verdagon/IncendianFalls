using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitCreateEffect : IUnitEffect {
  public readonly int id;
  public UnitCreateEffect(int id) {
    this.id = id;
  }
  int IUnitEffect.id => id;
  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitCreateEffect(this);
  }
}

}
