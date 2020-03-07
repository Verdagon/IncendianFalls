using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefyImpulseDeleteEffect : IDefyImpulseEffect {
  public readonly int id;
  public DefyImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyImpulseEffect.id => id;
  public void visit(IDefyImpulseEffectVisitor visitor) {
    visitor.visitDefyImpulseDeleteEffect(this);
  }
}

}
