using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BaseCombatTimeUCCreateEffect : IBaseCombatTimeUCEffect {
  public readonly int id;
  public BaseCombatTimeUCCreateEffect(int id) {
    this.id = id;
  }
  int IBaseCombatTimeUCEffect.id => id;
  public void visit(IBaseCombatTimeUCEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCCreateEffect(this);
  }
}

}
