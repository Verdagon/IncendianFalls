using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitMutSetAddEffect : IUnitMutSetEffect {
  public readonly int id;
  public readonly int element;
  public UnitMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IUnitMutSetEffect.id => id;
  public void visitIUnitMutSetEffect(IUnitMutSetEffectVisitor visitor) {
    visitor.visitUnitMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
