using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefyImpulseCreateEffect : IDefyImpulseEffect {
  public readonly int id;
  public DefyImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IDefyImpulseEffect.id => id;
  public void visit(IDefyImpulseEffectVisitor visitor) {
    visitor.visitDefyImpulseCreateEffect(this);
  }
}

}
