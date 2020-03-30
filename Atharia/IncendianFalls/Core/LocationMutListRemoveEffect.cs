using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListRemoveEffect : ILocationMutListEffect {
  public readonly int id;
  public readonly int index;
  public LocationMutListRemoveEffect(int id, int index) {
    this.id = id;
    this.index = index;
  }
  int ILocationMutListEffect.id => id;
  public void visitILocationMutListEffect(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLocationMutListEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
