using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCWeakMutSetAddEffect : ILightningChargedUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public LightningChargedUCWeakMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILightningChargedUCWeakMutSetEffect.id => id;
  public void visit(ILightningChargedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetAddEffect(this);
  }
}

}
