using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GuardAICapabilityUCIncarnation : IGuardAICapabilityUCEffectVisitor {
  public readonly Location guardCenterLocation;
  public readonly int guardRadius;
  public GuardAICapabilityUCIncarnation(
      Location guardCenterLocation,
      int guardRadius) {
    this.guardCenterLocation = guardCenterLocation;
    this.guardRadius = guardRadius;
  }
  public GuardAICapabilityUCIncarnation Copy() {
    return new GuardAICapabilityUCIncarnation(
guardCenterLocation,
guardRadius    );
  }

  public void visitGuardAICapabilityUCCreateEffect(GuardAICapabilityUCCreateEffect e) {}
  public void visitGuardAICapabilityUCDeleteEffect(GuardAICapabilityUCDeleteEffect e) {}


  public void ApplyEffect(IGuardAICapabilityUCEffect effect) { effect.visitIGuardAICapabilityUCEffect(this); }
}

}
