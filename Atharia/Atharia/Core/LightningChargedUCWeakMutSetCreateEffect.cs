using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCWeakMutSetCreateEffect : ILightningChargedUCWeakMutSetEffect {
  public readonly int id;
  public LightningChargedUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ILightningChargedUCWeakMutSetEffect.id => id;
  public void visitILightningChargedUCWeakMutSetEffect(ILightningChargedUCWeakMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
