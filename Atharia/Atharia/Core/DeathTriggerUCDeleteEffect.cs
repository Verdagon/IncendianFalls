using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DeathTriggerUCDeleteEffect : IDeathTriggerUCEffect {
  public readonly int id;
  public DeathTriggerUCDeleteEffect(int id) {
    this.id = id;
  }
  int IDeathTriggerUCEffect.id => id;
  public void visitIDeathTriggerUCEffect(IDeathTriggerUCEffectVisitor visitor) {
    visitor.visitDeathTriggerUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDeathTriggerUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
