using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitWeakMutSetRemoveEffect : IUnitWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public UnitWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IUnitWeakMutSetEffect.id => id;
  public void visitIUnitWeakMutSetEffect(IUnitWeakMutSetEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetEffect(this);
  }
}

}
