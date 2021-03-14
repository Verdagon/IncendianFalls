using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvolvifyAICapabilityUCMutSetDeleteEffect : IEvolvifyAICapabilityUCMutSetEffect {
  public readonly int id;
  public EvolvifyAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IEvolvifyAICapabilityUCMutSetEffect.id => id;
  public void visitIEvolvifyAICapabilityUCMutSetEffect(IEvolvifyAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
