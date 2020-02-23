using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BloodTTCMutSetRemoveEffect : IBloodTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BloodTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBloodTTCMutSetEffect.id => id;
  public void visit(IBloodTTCMutSetEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetRemoveEffect(this);
  }
}

}
