using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListCreateEffect : ILocationMutListEffect {
  public readonly int id;
  public LocationMutListCreateEffect(int id) {
    this.id = id;
  }
  int ILocationMutListEffect.id => id;
  public void visitILocationMutListEffect(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLocationMutListEffect(this);
  }
}

}
