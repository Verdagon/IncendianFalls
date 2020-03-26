using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseCombatTimeUCMutSetRemoveEffect : IBaseCombatTimeUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseCombatTimeUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseCombatTimeUCMutSetEffect.id => id;
  public void visitIBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetEffect(this);
  }
}

}
