using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCMutSetAddEffect : IAttackAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public AttackAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IAttackAICapabilityUCMutSetEffect.id => id;
  public void visitIAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
