using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseCombatTimeUCIncarnation : IBaseCombatTimeUCEffectVisitor {
  public readonly int combatTimeAddConstant;
  public readonly int combatTimeMultiplierPercent;
  public BaseCombatTimeUCIncarnation(
      int combatTimeAddConstant,
      int combatTimeMultiplierPercent) {
    this.combatTimeAddConstant = combatTimeAddConstant;
    this.combatTimeMultiplierPercent = combatTimeMultiplierPercent;
  }
  public BaseCombatTimeUCIncarnation Copy() {
    return new BaseCombatTimeUCIncarnation(
combatTimeAddConstant,
combatTimeMultiplierPercent    );
  }

  public void visitBaseCombatTimeUCCreateEffect(BaseCombatTimeUCCreateEffect e) {}
  public void visitBaseCombatTimeUCDeleteEffect(BaseCombatTimeUCDeleteEffect e) {}


  public void ApplyEffect(IBaseCombatTimeUCEffect effect) { effect.visitIBaseCombatTimeUCEffect(this); }
}

}
