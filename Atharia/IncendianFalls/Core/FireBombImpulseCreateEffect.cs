using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireBombImpulseCreateEffect : IFireBombImpulseEffect {
  public readonly int id;
  public readonly FireBombImpulseIncarnation incarnation;
  public FireBombImpulseCreateEffect(int id, FireBombImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IFireBombImpulseEffect.id => id;
  public void visitIFireBombImpulseEffect(IFireBombImpulseEffectVisitor visitor) {
    visitor.visitFireBombImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombImpulseEffect(this);
  }
}

}
