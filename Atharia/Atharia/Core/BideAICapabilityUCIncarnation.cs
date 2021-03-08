using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BideAICapabilityUCIncarnation : IBideAICapabilityUCEffectVisitor {
  public  int charge;
  public BideAICapabilityUCIncarnation(
      int charge) {
    this.charge = charge;
  }
  public BideAICapabilityUCIncarnation Copy() {
    return new BideAICapabilityUCIncarnation(
charge    );
  }

  public void visitBideAICapabilityUCCreateEffect(BideAICapabilityUCCreateEffect e) {}
  public void visitBideAICapabilityUCDeleteEffect(BideAICapabilityUCDeleteEffect e) {}
public void visitBideAICapabilityUCSetChargeEffect(BideAICapabilityUCSetChargeEffect e) { this.charge = e.newValue; }
  public void ApplyEffect(IBideAICapabilityUCEffect effect) { effect.visitIBideAICapabilityUCEffect(this); }
}

}
