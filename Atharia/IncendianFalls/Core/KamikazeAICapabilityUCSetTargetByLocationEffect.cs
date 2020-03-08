using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeAICapabilityUCSetTargetByLocationEffect : IKamikazeAICapabilityUCEffect {
  public readonly int id;
  public readonly KamikazeTargetTTCStrongByLocationMutMap newValue;
  public KamikazeAICapabilityUCSetTargetByLocationEffect(
      int id,
      KamikazeTargetTTCStrongByLocationMutMap newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IKamikazeAICapabilityUCEffect.id => id;

  public void visit(IKamikazeAICapabilityUCEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCSetTargetByLocationEffect(this);
  }
}

}
