using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BideAICapabilityUCMutSetRemoveEffect : IBideAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BideAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBideAICapabilityUCMutSetEffect.id => id;
  public void visitIBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetEffect(this);
  }
}

}
