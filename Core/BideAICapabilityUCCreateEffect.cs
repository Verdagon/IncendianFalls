using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BideAICapabilityUCCreateEffect : IBideAICapabilityUCEffect {
  public readonly int id;
  public BideAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int IBideAICapabilityUCEffect.id => id;
  public void visit(IBideAICapabilityUCEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCCreateEffect(this);
  }
}

}
