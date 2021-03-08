using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCMutSetAddEffect : ILightningChargedUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LightningChargedUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILightningChargedUCMutSetEffect.id => id;
  public void visitILightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
