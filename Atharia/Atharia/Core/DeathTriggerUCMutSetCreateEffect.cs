using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct DeathTriggerUCMutSetCreateEffect : IDeathTriggerUCMutSetEffect {
  public readonly int id;
  public DeathTriggerUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IDeathTriggerUCMutSetEffect.id => id;
  public void visitIDeathTriggerUCMutSetEffect(IDeathTriggerUCMutSetEffectVisitor visitor) {
    visitor.visitDeathTriggerUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDeathTriggerUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
