using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BideAICapabilityUCMutSetAddEffect : IBideAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BideAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBideAICapabilityUCMutSetEffect.id => id;
  public void visit(IBideAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetAddEffect(this);
  }
}

}
