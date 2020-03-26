using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitWeakMutSetAddEffect : IUnitWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public UnitWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IUnitWeakMutSetEffect.id => id;
  public void visitIUnitWeakMutSetEffect(IUnitWeakMutSetEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetEffect(this);
  }
}

}
