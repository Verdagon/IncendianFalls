using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BideAICapabilityUCMutSetCreateEffect : IBideAICapabilityUCMutSetEffect {
  public readonly int id;
  public BideAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBideAICapabilityUCMutSetEffect.id => id;
  public void visitIBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetEffect(this);
  }
}

}
