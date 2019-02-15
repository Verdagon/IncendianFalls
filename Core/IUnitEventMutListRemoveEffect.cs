using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IUnitEventMutListRemoveEffect : IIUnitEventMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public IUnitEventMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIUnitEventMutListEffect.id => id;
  public void visit(IIUnitEventMutListEffectVisitor visitor) {
    visitor.visitIUnitEventMutListRemoveEffect(this);
  }
}

}
