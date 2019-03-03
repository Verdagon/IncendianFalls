using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCWeakMutSetRemoveEffect : IAttackAICapabilityUCWeakMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public AttackAICapabilityUCWeakMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IAttackAICapabilityUCWeakMutSetEffect.id => id;
  public void visit(IAttackAICapabilityUCWeakMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCWeakMutSetRemoveEffect(this);
  }
}

}
