using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeAICapabilityUCMutSetRemoveEffect : IKamikazeAICapabilityUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeAICapabilityUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeAICapabilityUCMutSetEffect.id => id;
  public void visitIKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetEffect(this);
  }
}

}
