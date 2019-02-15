using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetLifeEndTimeEffect : IUnitEffect {
  public readonly int id;
  public readonly int newValue;
  public UnitSetLifeEndTimeEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetLifeEndTimeEffect(this);
  }
}
           
}
