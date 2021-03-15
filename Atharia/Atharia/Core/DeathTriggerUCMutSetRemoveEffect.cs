using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DeathTriggerUCMutSetRemoveEffect : IDeathTriggerUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public DeathTriggerUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IDeathTriggerUCMutSetEffect.id => id;
  public void visitIDeathTriggerUCMutSetEffect(IDeathTriggerUCMutSetEffectVisitor visitor) {
    visitor.visitDeathTriggerUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDeathTriggerUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
