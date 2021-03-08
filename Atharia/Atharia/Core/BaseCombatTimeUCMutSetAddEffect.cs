using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseCombatTimeUCMutSetAddEffect : IBaseCombatTimeUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseCombatTimeUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseCombatTimeUCMutSetEffect.id => id;
  public void visitIBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseCombatTimeUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
