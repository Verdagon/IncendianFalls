using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListAddEffect : ILocationMutListEffect {
  public readonly int id;
  public readonly Location element;
  public LocationMutListAddEffect(int id, Location element) {
    this.id = id;
    this.element = element;
  }
  int ILocationMutListEffect.id => id;
  public void visit(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListAddEffect(this);
  }
}

}
