using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCWeakMutSetAddEffect : ILightningChargedUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public LightningChargedUCWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ILightningChargedUCWeakMutSetEffect.id => id;
  public void visitILightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
