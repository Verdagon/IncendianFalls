using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TemporaryCloneAICapabilityUCCreateEffect : ITemporaryCloneAICapabilityUCEffect {
  public readonly int id;
  public TemporaryCloneAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int ITemporaryCloneAICapabilityUCEffect.id => id;
  public void visit(ITemporaryCloneAICapabilityUCEffectVisitor visitor) {
    visitor.visitTemporaryCloneAICapabilityUCCreateEffect(this);
  }
}

}
