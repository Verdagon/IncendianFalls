using System;
using System.Collections.Generic;

namespace Atharia.Model {
public struct UnitMutSetRemoveEffect : IUnitMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UnitMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUnitMutSetEffect.id => id;
  public void visit(IUnitMutSetEffectVisitor visitor) {
    visitor.visitUnitMutSetRemoveEffect(this);
  }
}

}
