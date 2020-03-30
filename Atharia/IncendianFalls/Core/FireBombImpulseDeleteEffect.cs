using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireBombImpulseDeleteEffect : IFireBombImpulseEffect {
  public readonly int id;
  public FireBombImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IFireBombImpulseEffect.id => id;
  public void visitIFireBombImpulseEffect(IFireBombImpulseEffectVisitor visitor) {
    visitor.visitFireBombImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombImpulseEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
