using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BideAICapabilityUCMutSetAddEffect : IBideAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BideAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBideAICapabilityUCMutSetEffect.id => id;
  public void visitIBideAICapabilityUCMutSetEffect(IBideAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
