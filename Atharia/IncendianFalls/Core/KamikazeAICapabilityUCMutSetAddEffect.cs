using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeAICapabilityUCMutSetAddEffect : IKamikazeAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeAICapabilityUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeAICapabilityUCMutSetEffect.id => id;
  public void visitIKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetEffect(this);
  }
}

}
