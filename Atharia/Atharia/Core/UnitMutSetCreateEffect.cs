using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitMutSetCreateEffect : IUnitMutSetEffect {
  public readonly int id;
  public UnitMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IUnitMutSetEffect.id => id;
  public void visitIUnitMutSetEffect(IUnitMutSetEffectVisitor visitor) {
    visitor.visitUnitMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
