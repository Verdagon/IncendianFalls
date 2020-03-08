using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeAICapabilityUCMutSetDeleteEffect : IKamikazeAICapabilityUCMutSetEffect {
  public readonly int id;
  public KamikazeAICapabilityUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeAICapabilityUCMutSetEffect.id => id;
  public void visit(IKamikazeAICapabilityUCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCMutSetDeleteEffect(this);
  }
}

}
