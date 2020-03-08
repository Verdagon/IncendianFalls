using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DefyImpulseStrongMutSetDeleteEffect : IDefyImpulseStrongMutSetEffect {
  public readonly int id;
  public DefyImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDefyImpulseStrongMutSetEffect.id => id;
  public void visit(IDefyImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitDefyImpulseStrongMutSetDeleteEffect(this);
  }
}

}
