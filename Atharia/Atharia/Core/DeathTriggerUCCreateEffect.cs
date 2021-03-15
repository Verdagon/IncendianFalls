using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DeathTriggerUCCreateEffect : IDeathTriggerUCEffect {
  public readonly int id;
  public readonly DeathTriggerUCIncarnation incarnation;
  public DeathTriggerUCCreateEffect(int id, DeathTriggerUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDeathTriggerUCEffect.id => id;
  public void visitIDeathTriggerUCEffect(IDeathTriggerUCEffectVisitor visitor) {
    visitor.visitDeathTriggerUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDeathTriggerUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
