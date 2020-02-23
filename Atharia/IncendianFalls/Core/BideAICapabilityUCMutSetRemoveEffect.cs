using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BideAICapabilityUCMutSetRemoveEffect : IBideAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BideAICapabilityUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBideAICapabilityUCMutSetEffect.id => id;
  public void visit(IBideAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetRemoveEffect(this);
  }
}

}
