using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonAICapabilityUCIncarnation : ISummonAICapabilityUCEffectVisitor {
  public readonly string blueprintName;
  public  int charges;
  public SummonAICapabilityUCIncarnation(
      string blueprintName,
      int charges) {
    this.blueprintName = blueprintName;
    this.charges = charges;
  }
  public SummonAICapabilityUCIncarnation Copy() {
    return new SummonAICapabilityUCIncarnation(
blueprintName,
charges    );
  }

  public void visitSummonAICapabilityUCCreateEffect(SummonAICapabilityUCCreateEffect e) {}
  public void visitSummonAICapabilityUCDeleteEffect(SummonAICapabilityUCDeleteEffect e) {}

public void visitSummonAICapabilityUCSetChargesEffect(SummonAICapabilityUCSetChargesEffect e) { this.charges = e.newValue; }
  public void ApplyEffect(ISummonAICapabilityUCEffect effect) { effect.visitISummonAICapabilityUCEffect(this); }
}

}
