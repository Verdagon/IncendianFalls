using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BideAICapabilityUCMutSetCreateEffect : IBideAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly BideAICapabilityUCMutSetIncarnation incarnation;
  public BideAICapabilityUCMutSetCreateEffect(
      int id,
      BideAICapabilityUCMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBideAICapabilityUCMutSetEffect.id => id;
  public void visit(IBideAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetCreateEffect(this);
  }
}

}
