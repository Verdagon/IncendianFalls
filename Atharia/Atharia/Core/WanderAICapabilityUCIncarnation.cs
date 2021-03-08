using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WanderAICapabilityUCIncarnation : IWanderAICapabilityUCEffectVisitor {
  public WanderAICapabilityUCIncarnation(
) {
  }
  public WanderAICapabilityUCIncarnation Copy() {
    return new WanderAICapabilityUCIncarnation(
    );
  }

  public void visitWanderAICapabilityUCCreateEffect(WanderAICapabilityUCCreateEffect e) {}
  public void visitWanderAICapabilityUCDeleteEffect(WanderAICapabilityUCDeleteEffect e) {}

  public void ApplyEffect(IWanderAICapabilityUCEffect effect) { effect.visitIWanderAICapabilityUCEffect(this); }
}

}
