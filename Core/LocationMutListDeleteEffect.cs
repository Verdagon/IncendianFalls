using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListDeleteEffect : ILocationMutListEffect {
  public readonly int id;
  public LocationMutListDeleteEffect(int id) {
    this.id = id;
  }
  int ILocationMutListEffect.id => id;
  public void visit(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListDeleteEffect(this);
  }
}

}
