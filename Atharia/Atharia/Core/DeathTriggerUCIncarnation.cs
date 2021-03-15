using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DeathTriggerUCIncarnation : IDeathTriggerUCEffectVisitor {
  public readonly string triggerName;
  public DeathTriggerUCIncarnation(
      string triggerName) {
    this.triggerName = triggerName;
  }
  public DeathTriggerUCIncarnation Copy() {
    return new DeathTriggerUCIncarnation(
triggerName    );
  }

  public void visitDeathTriggerUCCreateEffect(DeathTriggerUCCreateEffect e) {}
  public void visitDeathTriggerUCDeleteEffect(DeathTriggerUCDeleteEffect e) {}

  public void ApplyEffect(IDeathTriggerUCEffect effect) { effect.visitIDeathTriggerUCEffect(this); }
}

}
