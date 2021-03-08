using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LightningChargedUCIncarnation : ILightningChargedUCEffectVisitor {
  public LightningChargedUCIncarnation(
) {
  }
  public LightningChargedUCIncarnation Copy() {
    return new LightningChargedUCIncarnation(
    );
  }

  public void visitLightningChargedUCCreateEffect(LightningChargedUCCreateEffect e) {}
  public void visitLightningChargedUCDeleteEffect(LightningChargedUCDeleteEffect e) {}

  public void ApplyEffect(ILightningChargedUCEffect effect) { effect.visitILightningChargedUCEffect(this); }
}

}
