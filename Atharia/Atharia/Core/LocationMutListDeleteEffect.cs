using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LocationMutListDeleteEffect : ILocationMutListEffect {
  public readonly int id;
  public LocationMutListDeleteEffect(int id) {
    this.id = id;
  }
  int ILocationMutListEffect.id => id;
  public void visitILocationMutListEffect(ILocationMutListEffectVisitor visitor) {
    visitor.visitLocationMutListDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLocationMutListEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
