using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireTTCMutSetRemoveEffect : IFireTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public FireTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IFireTTCMutSetEffect.id => id;
  public void visit(IFireTTCMutSetEffectVisitor visitor) {
    visitor.visitFireTTCMutSetRemoveEffect(this);
  }
}

}
