using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCWeakMutSetRemoveEffect : ILightningChargedUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public LightningChargedUCWeakMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILightningChargedUCWeakMutSetEffect.id => id;
  public void visit(ILightningChargedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetRemoveEffect(this);
  }
}

}
