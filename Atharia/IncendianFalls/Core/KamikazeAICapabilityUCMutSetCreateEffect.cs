using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeAICapabilityUCMutSetCreateEffect : IKamikazeAICapabilityUCMutSetEffect {
  public readonly int id;
  public KamikazeAICapabilityUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeAICapabilityUCMutSetEffect.id => id;
  public void visitIKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
