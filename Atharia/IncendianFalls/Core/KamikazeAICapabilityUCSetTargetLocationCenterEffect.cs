using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeAICapabilityUCSetTargetLocationCenterEffect : IKamikazeAICapabilityUCEffect {
  public readonly int id;
  public readonly Location newValue;
  public KamikazeAICapabilityUCSetTargetLocationCenterEffect(
      int id,
      Location newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IKamikazeAICapabilityUCEffect.id => id;

  public void visit(IKamikazeAICapabilityUCEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCSetTargetLocationCenterEffect(this);
  }
}

}
