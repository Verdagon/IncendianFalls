using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LightningChargedUCCreateEffect : ILightningChargedUCEffect {
  public readonly int id;
  public readonly LightningChargedUCIncarnation incarnation;
  public LightningChargedUCCreateEffect(int id, LightningChargedUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ILightningChargedUCEffect.id => id;
  public void visitILightningChargedUCEffect(ILightningChargedUCEffectVisitor visitor) {
    visitor.visitLightningChargedUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitLightningChargedUCEffect(this);
  }
}

}
