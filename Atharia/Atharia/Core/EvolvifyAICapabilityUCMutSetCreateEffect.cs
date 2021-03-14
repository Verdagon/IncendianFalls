using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvolvifyAICapabilityUCMutSetCreateEffect : IEvolvifyAICapabilityUCMutSetEffect {
  public readonly int id;
  public EvolvifyAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IEvolvifyAICapabilityUCMutSetEffect.id => id;
  public void visitIEvolvifyAICapabilityUCMutSetEffect(IEvolvifyAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
