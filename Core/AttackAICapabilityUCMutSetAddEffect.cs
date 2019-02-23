using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct AttackAICapabilityUCMutSetAddEffect : IAttackAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public AttackAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IAttackAICapabilityUCMutSetEffect.id => id;
  public void visit(IAttackAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitAttackAICapabilityUCMutSetAddEffect(this);
  }
}

}
