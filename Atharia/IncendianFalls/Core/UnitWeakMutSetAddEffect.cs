using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitWeakMutSetAddEffect : IUnitWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UnitWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUnitWeakMutSetEffect.id => id;
  public void visit(IUnitWeakMutSetEffectVisitor visitor) {
    visitor.visitUnitWeakMutSetAddEffect(this);
  }
}

}
