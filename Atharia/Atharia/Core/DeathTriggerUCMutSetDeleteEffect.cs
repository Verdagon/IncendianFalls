using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DeathTriggerUCMutSetDeleteEffect : IDeathTriggerUCMutSetEffect {
  public readonly int id;
  public DeathTriggerUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IDeathTriggerUCMutSetEffect.id => id;
  public void visitIDeathTriggerUCMutSetEffect(IDeathTriggerUCMutSetEffectVisitor visitor) {
    visitor.visitDeathTriggerUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDeathTriggerUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
