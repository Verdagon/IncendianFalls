using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetMpEffect : IUnitEffect {
  public readonly int id;
  public readonly int newValue;
  public UnitSetMpEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetMpEffect(this);
  }
}

}
