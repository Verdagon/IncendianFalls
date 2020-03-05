using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BaseCombatTimeUCIncarnation {
  public readonly int combatTimeAddConstant;
  public readonly int combatTimeMultiplierPercent;
  public BaseCombatTimeUCIncarnation(
      int combatTimeAddConstant,
      int combatTimeMultiplierPercent) {
    this.combatTimeAddConstant = combatTimeAddConstant;
    this.combatTimeMultiplierPercent = combatTimeMultiplierPercent;
  }
}

}
