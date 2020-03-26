using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCMutSetRemoveEffect : ILightningChargedUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LightningChargedUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILightningChargedUCMutSetEffect.id => id;
  public void visitILightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetEffect(this);
  }
}

}
