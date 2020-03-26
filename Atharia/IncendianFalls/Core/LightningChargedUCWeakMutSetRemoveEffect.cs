using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCWeakMutSetRemoveEffect : ILightningChargedUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LightningChargedUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILightningChargedUCWeakMutSetEffect.id => id;
  public void visitILightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetEffect(this);
  }
}

}
