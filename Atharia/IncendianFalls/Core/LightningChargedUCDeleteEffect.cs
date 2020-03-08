using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LightningChargedUCDeleteEffect : ILightningChargedUCEffect {
  public readonly int id;
  public LightningChargedUCDeleteEffect(int id) {
    this.id = id;
  }
  int ILightningChargedUCEffect.id => id;
  public void visit(ILightningChargedUCEffectVisitor visitor) {
    visitor.visitLightningChargedUCDeleteEffect(this);
  }
}

}
