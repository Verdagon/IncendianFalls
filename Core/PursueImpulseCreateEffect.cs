using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PursueImpulseCreateEffect : IPursueImpulseEffect {
  public readonly int id;
  public PursueImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IPursueImpulseEffect.id => id;
  public void visit(IPursueImpulseEffectVisitor visitor) {
    visitor.visitPursueImpulseCreateEffect(this);
  }
}

}
