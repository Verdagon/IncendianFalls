using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCMutSetAddEffect : ILightningChargedUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public LightningChargedUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ILightningChargedUCMutSetEffect.id => id;
  public void visit(ILightningChargedUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetAddEffect(this);
  }
}

}
