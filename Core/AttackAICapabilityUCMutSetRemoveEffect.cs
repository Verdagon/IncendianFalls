using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCMutSetRemoveEffect : IAttackAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public AttackAICapabilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IAttackAICapabilityUCMutSetEffect.id => id;
  public void visit(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetRemoveEffect(this);
  }
}

}
