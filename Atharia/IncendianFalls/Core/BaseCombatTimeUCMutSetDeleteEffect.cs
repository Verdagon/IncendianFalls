using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseCombatTimeUCMutSetDeleteEffect : IBaseCombatTimeUCMutSetEffect {
  public readonly int id;
  public BaseCombatTimeUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseCombatTimeUCMutSetEffect.id => id;
  public void visitIBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetEffect(this);
  }
}

}
