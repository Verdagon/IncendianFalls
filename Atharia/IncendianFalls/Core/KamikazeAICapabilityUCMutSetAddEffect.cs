using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeAICapabilityUCMutSetAddEffect : IKamikazeAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public KamikazeAICapabilityUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IKamikazeAICapabilityUCMutSetEffect.id => id;
  public void visit(IKamikazeAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetAddEffect(this);
  }
}

}
