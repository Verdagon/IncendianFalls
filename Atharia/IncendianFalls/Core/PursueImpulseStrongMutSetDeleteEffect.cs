using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct PursueImpulseStrongMutSetDeleteEffect : IPursueImpulseStrongMutSetEffect {
  public readonly int id;
  public PursueImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IPursueImpulseStrongMutSetEffect.id => id;
  public void visit(IPursueImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitPursueImpulseStrongMutSetDeleteEffect(this);
  }
}

}
