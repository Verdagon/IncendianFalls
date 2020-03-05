using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseCombatTimeUCMutSetAddEffect : IBaseCombatTimeUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BaseCombatTimeUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBaseCombatTimeUCMutSetEffect.id => id;
  public void visit(IBaseCombatTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetAddEffect(this);
  }
}

}
