using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombTTCMutSetRemoveEffect : IFireBombTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FireBombTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFireBombTTCMutSetEffect.id => id;
  public void visit(IFireBombTTCMutSetEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetRemoveEffect(this);
  }
}

}
