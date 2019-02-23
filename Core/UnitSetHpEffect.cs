using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetHpEffect : IUnitEffect {
  public readonly int id;
  public readonly int newValue;
  public UnitSetHpEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetHpEffect(this);
  }
}
           
}
