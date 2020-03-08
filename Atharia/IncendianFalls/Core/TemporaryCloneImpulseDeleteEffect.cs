using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TemporaryCloneImpulseDeleteEffect : ITemporaryCloneImpulseEffect {
  public readonly int id;
  public TemporaryCloneImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneImpulseEffect.id => id;
  public void visit(ITemporaryCloneImpulseEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseDeleteEffect(this);
  }
}

}
