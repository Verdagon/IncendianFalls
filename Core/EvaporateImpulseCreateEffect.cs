using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EvaporateImpulseCreateEffect : IEvaporateImpulseEffect {
  public readonly int id;
  public EvaporateImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IEvaporateImpulseEffect.id => id;
  public void visit(IEvaporateImpulseEffectVisitor visitor) {
    visitor.visitEvaporateImpulseCreateEffect(this);
  }
}

}
