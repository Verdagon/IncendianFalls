using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListRemoveEffect : ILocationMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public LocationMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILocationMutListEffect.id => id;
  public void visit(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListRemoveEffect(this);
  }
}

}
