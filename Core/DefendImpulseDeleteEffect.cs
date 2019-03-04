using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DefendImpulseDeleteEffect : IDefendImpulseEffect {
  public readonly int id;
  public DefendImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IDefendImpulseEffect.id => id;
  public void visit(IDefendImpulseEffectVisitor visitor) {
    visitor.visitDefendImpulseDeleteEffect(this);
  }
}

}
