using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCMutSetRemoveEffect : IAttackAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public AttackAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IAttackAICapabilityUCMutSetEffect.id => id;
  public void visitIAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
