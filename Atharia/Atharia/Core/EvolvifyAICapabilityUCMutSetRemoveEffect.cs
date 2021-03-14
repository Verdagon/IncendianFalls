using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvolvifyAICapabilityUCMutSetRemoveEffect : IEvolvifyAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public EvolvifyAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IEvolvifyAICapabilityUCMutSetEffect.id => id;
  public void visitIEvolvifyAICapabilityUCMutSetEffect(IEvolvifyAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
