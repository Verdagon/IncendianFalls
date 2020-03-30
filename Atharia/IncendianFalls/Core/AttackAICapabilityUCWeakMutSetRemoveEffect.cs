using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCWeakMutSetRemoveEffect : IAttackAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public AttackAICapabilityUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IAttackAICapabilityUCWeakMutSetEffect.id => id;
  public void visitIAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
