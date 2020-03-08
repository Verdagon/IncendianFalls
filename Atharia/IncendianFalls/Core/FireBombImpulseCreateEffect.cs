using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireBombImpulseCreateEffect : IFireBombImpulseEffect {
  public readonly int id;
  public FireBombImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IFireBombImpulseEffect.id => id;
  public void visit(IFireBombImpulseEffectVisitor visitor) {
    visitor.visitFireBombImpulseCreateEffect(this);
  }
}

}
