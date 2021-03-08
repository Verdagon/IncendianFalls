using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCMutSetCreateEffect : IAttackAICapabilityUCMutSetEffect {
  public readonly int id;
  public AttackAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IAttackAICapabilityUCMutSetEffect.id => id;
  public void visitIAttackAICapabilityUCMutSetEffect(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
