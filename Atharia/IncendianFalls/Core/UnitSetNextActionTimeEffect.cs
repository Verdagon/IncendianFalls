using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetNextActionTimeEffect : IUnitEffect {
  public readonly int id;
  public readonly int newValue;
  public UnitSetNextActionTimeEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visitIUnitEffect(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetNextActionTimeEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitEffect(this);
  }
}

}
