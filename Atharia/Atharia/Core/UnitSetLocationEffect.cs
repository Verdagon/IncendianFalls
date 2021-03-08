using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnitSetLocationEffect : IUnitEffect {
  public readonly int id;
  public readonly Location newValue;
  public UnitSetLocationEffect(
      int id,
      Location newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IUnitEffect.id => id;

  public void visitIUnitEffect(IUnitEffectVisitor visitor) {
    visitor.visitUnitSetLocationEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
