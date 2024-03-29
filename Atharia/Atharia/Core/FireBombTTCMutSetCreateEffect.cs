using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombTTCMutSetCreateEffect : IFireBombTTCMutSetEffect {
  public readonly int id;
  public FireBombTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IFireBombTTCMutSetEffect.id => id;
  public void visitIFireBombTTCMutSetEffect(IFireBombTTCMutSetEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
