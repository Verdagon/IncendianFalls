using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BideAICapabilityUCDeleteEffect : IBideAICapabilityUCEffect {
  public readonly int id;
  public BideAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBideAICapabilityUCEffect.id => id;
  public void visit(IBideAICapabilityUCEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCDeleteEffect(this);
  }
}
       
}
