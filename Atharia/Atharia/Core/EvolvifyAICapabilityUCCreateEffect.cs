using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct EvolvifyAICapabilityUCCreateEffect : IEvolvifyAICapabilityUCEffect {
  public readonly int id;
  public readonly EvolvifyAICapabilityUCIncarnation incarnation;
  public EvolvifyAICapabilityUCCreateEffect(int id, EvolvifyAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IEvolvifyAICapabilityUCEffect.id => id;
  public void visitIEvolvifyAICapabilityUCEffect(IEvolvifyAICapabilityUCEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitEvolvifyAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
