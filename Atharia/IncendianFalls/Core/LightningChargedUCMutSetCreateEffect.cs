using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct LightningChargedUCMutSetCreateEffect : ILightningChargedUCMutSetEffect {
  public readonly int id;
  public LightningChargedUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ILightningChargedUCMutSetEffect.id => id;
  public void visit(ILightningChargedUCMutSetEffectVisitor visitor) {
    visitor.visitLightningChargedUCMutSetCreateEffect(this);
  }
}

}