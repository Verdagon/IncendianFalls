using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvolvifyAICapabilityUCMutSetAddEffect : IEvolvifyAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public EvolvifyAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IEvolvifyAICapabilityUCMutSetEffect.id => id;
  public void visitIEvolvifyAICapabilityUCMutSetEffect(IEvolvifyAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
