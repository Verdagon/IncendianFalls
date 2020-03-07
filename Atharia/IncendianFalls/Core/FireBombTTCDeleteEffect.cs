using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireBombTTCDeleteEffect : IFireBombTTCEffect {
  public readonly int id;
  public FireBombTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IFireBombTTCEffect.id => id;
  public void visit(IFireBombTTCEffectVisitor visitor) {
    visitor.visitFireBombTTCDeleteEffect(this);
  }
}

}
