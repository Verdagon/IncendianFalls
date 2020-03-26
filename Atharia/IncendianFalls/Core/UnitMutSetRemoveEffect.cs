using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitMutSetRemoveEffect : IUnitMutSetEffect {
  public readonly int id;
  public readonly int element;
  public UnitMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IUnitMutSetEffect.id => id;
  public void visitIUnitMutSetEffect(IUnitMutSetEffectVisitor visitor) {
    visitor.visitUnitMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitMutSetEffect(this);
  }
}

}
