using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombTTCMutSetAddEffect : IFireBombTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireBombTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireBombTTCMutSetEffect.id => id;
  public void visitIFireBombTTCMutSetEffect(IFireBombTTCMutSetEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
