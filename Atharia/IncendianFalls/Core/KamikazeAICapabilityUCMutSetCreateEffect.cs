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
  public void visit(IKamikazeAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetCreateEffect(this);
  }
}

}
