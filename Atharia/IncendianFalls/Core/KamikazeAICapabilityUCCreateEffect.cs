using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeAICapabilityUCCreateEffect : IKamikazeAICapabilityUCEffect {
  public readonly int id;
  public readonly KamikazeAICapabilityUCIncarnation incarnation;
  public KamikazeAICapabilityUCCreateEffect(int id, KamikazeAICapabilityUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IKamikazeAICapabilityUCEffect.id => id;
  public void visitIKamikazeAICapabilityUCEffect(IKamikazeAICapabilityUCEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCEffect(this);
  }
}

}
