using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombTTCMutSetDeleteEffect : IFireBombTTCMutSetEffect {
  public readonly int id;
  public FireBombTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IFireBombTTCMutSetEffect.id => id;
  public void visitIFireBombTTCMutSetEffect(IFireBombTTCMutSetEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
