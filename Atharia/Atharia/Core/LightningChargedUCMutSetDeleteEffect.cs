using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCMutSetDeleteEffect : ILightningChargedUCMutSetEffect {
  public readonly int id;
  public LightningChargedUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ILightningChargedUCMutSetEffect.id => id;
  public void visitILightningChargedUCMutSetEffect(ILightningChargedUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
