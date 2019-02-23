using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BideAICapabilityUCCreateEffect : IBideAICapabilityUCEffect {
  public readonly int id;
  public readonly BideAICapabilityUCIncarnation incarnation;
  public BideAICapabilityUCCreateEffect(
      int id,
      BideAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBideAICapabilityUCEffect.id => id;
  public void visit(IBideAICapabilityUCEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCCreateEffect(this);
  }
}
       
}
