using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BideAICapabilityUCMutSetDeleteEffect : IBideAICapabilityUCMutSetEffect {
  public readonly int id;
  public BideAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBideAICapabilityUCMutSetEffect.id => id;
  public void visit(IBideAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCMutSetDeleteEffect(this);
  }
}

}
