using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireBombTTCMutSetAddEffect : IFireBombTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FireBombTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFireBombTTCMutSetEffect.id => id;
  public void visit(IFireBombTTCMutSetEffectVisitor visitor) {
    visitor.visitFireBombTTCMutSetAddEffect(this);
  }
}

}
