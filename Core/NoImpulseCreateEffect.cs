using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct NoImpulseCreateEffect : INoImpulseEffect {
  public readonly int id;
  public NoImpulseCreateEffect(int id) {
    this.id = id;
  }
  int INoImpulseEffect.id => id;
  public void visit(INoImpulseEffectVisitor visitor) {
    visitor.visitNoImpulseCreateEffect(this);
  }
}

}
