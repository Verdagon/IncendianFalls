using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IDetailMutListRemoveEffect : IIDetailMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public IDetailMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIDetailMutListEffect.id => id;
  public void visit(IIDetailMutListEffectVisitor visitor) {
    visitor.visitIDetailMutListRemoveEffect(this);
  }
}

}
