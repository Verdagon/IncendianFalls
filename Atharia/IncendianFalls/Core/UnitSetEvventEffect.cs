using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetEvventEffect : IUnitEffect {
  public readonly int id;
  public readonly IUnitEvent newValue;
  public UnitSetEvventEffect(
      int id,
      IUnitEvent newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visitIUnitEffect(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetEvventEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitEffect(this);
  }
}

}
