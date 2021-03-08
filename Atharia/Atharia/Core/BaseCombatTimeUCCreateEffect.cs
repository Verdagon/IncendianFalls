using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseCombatTimeUCCreateEffect : IBaseCombatTimeUCEffect {
  public readonly int id;
  public readonly BaseCombatTimeUCIncarnation incarnation;
  public BaseCombatTimeUCCreateEffect(int id, BaseCombatTimeUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBaseCombatTimeUCEffect.id => id;
  public void visitIBaseCombatTimeUCEffect(IBaseCombatTimeUCEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
