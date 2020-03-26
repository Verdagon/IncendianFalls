using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseCombatTimeUCMutSetCreateEffect : IBaseCombatTimeUCMutSetEffect {
  public readonly int id;
  public BaseCombatTimeUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBaseCombatTimeUCMutSetEffect.id => id;
  public void visitIBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetEffect(this);
  }
}

}
