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
  public void visitIFireBombTTCEffect(IFireBombTTCEffectVisitor visitor) {
    visitor.visitFireBombTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombTTCEffect(this);
  }
}

}
