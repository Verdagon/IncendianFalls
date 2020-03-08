using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeAICapabilityUCCreateEffect : IKamikazeAICapabilityUCEffect {
  public readonly int id;
  public KamikazeAICapabilityUCCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeAICapabilityUCEffect.id => id;
  public void visit(IKamikazeAICapabilityUCEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCCreateEffect(this);
  }
}

}
