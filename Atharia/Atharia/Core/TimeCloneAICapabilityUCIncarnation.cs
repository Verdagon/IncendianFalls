using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeCloneAICapabilityUCIncarnation : ITimeCloneAICapabilityUCEffectVisitor {
  public readonly int script;
  public TimeCloneAICapabilityUCIncarnation(
      int script) {
    this.script = script;
  }
  public TimeCloneAICapabilityUCIncarnation Copy() {
    return new TimeCloneAICapabilityUCIncarnation(
script    );
  }

  public void visitTimeCloneAICapabilityUCCreateEffect(TimeCloneAICapabilityUCCreateEffect e) {}
  public void visitTimeCloneAICapabilityUCDeleteEffect(TimeCloneAICapabilityUCDeleteEffect e) {}

  public void ApplyEffect(ITimeCloneAICapabilityUCEffect effect) { effect.visitITimeCloneAICapabilityUCEffect(this); }
}

}
