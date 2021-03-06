using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseCombatTimeUCDeleteEffect : IBaseCombatTimeUCEffect {
  public readonly int id;
  public BaseCombatTimeUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBaseCombatTimeUCEffect.id => id;
  public void visitIBaseCombatTimeUCEffect(IBaseCombatTimeUCEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
