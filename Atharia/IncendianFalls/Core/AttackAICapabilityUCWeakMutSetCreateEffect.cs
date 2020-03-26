using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCWeakMutSetCreateEffect : IAttackAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public AttackAICapabilityUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IAttackAICapabilityUCWeakMutSetEffect.id => id;
  public void visitIAttackAICapabilityUCWeakMutSetEffect(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetEffect(this);
  }
}

}
