using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IRequestMutListRemoveEffect : IIRequestMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public IRequestMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIRequestMutListEffect.id => id;
  public void visit(IIRequestMutListEffectVisitor visitor) {
    visitor.visitIRequestMutListRemoveEffect(this);
  }
}

}
