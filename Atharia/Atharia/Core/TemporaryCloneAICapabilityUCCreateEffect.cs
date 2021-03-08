using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TemporaryCloneAICapabilityUCCreateEffect : ITemporaryCloneAICapabilityUCEffect {
  public readonly int id;
  public readonly TemporaryCloneAICapabilityUCIncarnation incarnation;
  public TemporaryCloneAICapabilityUCCreateEffect(int id, TemporaryCloneAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITemporaryCloneAICapabilityUCEffect.id => id;
  public void visitITemporaryCloneAICapabilityUCEffect(ITemporaryCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
