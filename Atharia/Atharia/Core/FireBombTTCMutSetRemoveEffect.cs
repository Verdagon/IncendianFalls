using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombTTCMutSetRemoveEffect : IFireBombTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireBombTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireBombTTCMutSetEffect.id => id;
  public void visitIFireBombTTCMutSetEffect(IFireBombTTCMutSetEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
