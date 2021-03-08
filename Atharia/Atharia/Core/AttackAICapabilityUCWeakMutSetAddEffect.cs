using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCWeakMutSetAddEffect : IAttackAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public AttackAICapabilityUCWeakMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IAttackAICapabilityUCWeakMutSetEffect.id => id;
  public void visitIAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
