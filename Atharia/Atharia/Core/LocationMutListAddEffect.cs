using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListAddEffect : ILocationMutListEffect {
  public readonly int id;
  public readonly int index;
  public readonly Location element;
  public LocationMutListAddEffect(int id, int index, Location element) {
    this.id = id;
    this.index = index;
    this.element = element;
  }
  int ILocationMutListEffect.id => id;
  public void visitILocationMutListEffect(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLocationMutListEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
