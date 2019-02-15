using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListCreateEffect : ILocationMutListEffect {
  public readonly int id;
  public readonly LocationMutListIncarnation incarnation;
  public LocationMutListCreateEffect(
      int id,
      LocationMutListIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILocationMutListEffect.id => id;
  public void visit(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListCreateEffect(this);
  }
}

}
