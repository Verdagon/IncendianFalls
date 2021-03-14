using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvolvifyAICapabilityUCIncarnation : IEvolvifyAICapabilityUCEffectVisitor {
  public EvolvifyAICapabilityUCIncarnation(
) {
  }
  public EvolvifyAICapabilityUCIncarnation Copy() {
    return new EvolvifyAICapabilityUCIncarnation(
    );
  }

  public void visitEvolvifyAICapabilityUCCreateEffect(EvolvifyAICapabilityUCCreateEffect e) {}
  public void visitEvolvifyAICapabilityUCDeleteEffect(EvolvifyAICapabilityUCDeleteEffect e) {}

  public void ApplyEffect(IEvolvifyAICapabilityUCEffect effect) { effect.visitIEvolvifyAICapabilityUCEffect(this); }
}

}
