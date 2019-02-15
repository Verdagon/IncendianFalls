using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetAliveEffect : IUnitEffect {
  public readonly int id;
  public readonly bool newValue;
  public UnitSetAliveEffect(
      int id,
      bool newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetAliveEffect(this);
  }
}
           
}
