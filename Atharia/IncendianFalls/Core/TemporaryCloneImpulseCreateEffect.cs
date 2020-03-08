using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TemporaryCloneImpulseCreateEffect : ITemporaryCloneImpulseEffect {
  public readonly int id;
  public TemporaryCloneImpulseCreateEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneImpulseEffect.id => id;
  public void visit(ITemporaryCloneImpulseEffectVisitor visitor) {
    visitor.visitTemporaryCloneImpulseCreateEffect(this);
  }
}

}
