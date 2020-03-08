using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LightningChargedUCCreateEffect : ILightningChargedUCEffect {
  public readonly int id;
  public LightningChargedUCCreateEffect(int id) {
    this.id = id;
  }
  int ILightningChargedUCEffect.id => id;
  public void visit(ILightningChargedUCEffectVisitor visitor) {
    visitor.visitLightningChargedUCCreateEffect(this);
  }
}

}
