using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TemporaryCloneAICapabilityUCIncarnation : ITemporaryCloneAICapabilityUCEffectVisitor {
  public readonly string blueprintName;
  public  int charges;
  public TemporaryCloneAICapabilityUCIncarnation(
      string blueprintName,
      int charges) {
    this.blueprintName = blueprintName;
    this.charges = charges;
  }
  public TemporaryCloneAICapabilityUCIncarnation Copy() {
    return new TemporaryCloneAICapabilityUCIncarnation(
blueprintName,
charges    );
  }

  public void visitTemporaryCloneAICapabilityUCCreateEffect(TemporaryCloneAICapabilityUCCreateEffect e) {}
  public void visitTemporaryCloneAICapabilityUCDeleteEffect(TemporaryCloneAICapabilityUCDeleteEffect e) {}

public void visitTemporaryCloneAICapabilityUCSetChargesEffect(TemporaryCloneAICapabilityUCSetChargesEffect e) { this.charges = e.newValue; }
  public void ApplyEffect(ITemporaryCloneAICapabilityUCEffect effect) { effect.visitITemporaryCloneAICapabilityUCEffect(this); }
}

}
