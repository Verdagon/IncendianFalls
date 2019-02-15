using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetDirectiveEffect : IUnitEffect {
  public readonly int id;
  public readonly IDirective newValue;
  public UnitSetDirectiveEffect(
      int id,
      IDirective newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visit(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetDirectiveEffect(this);
  }
}
           
}
