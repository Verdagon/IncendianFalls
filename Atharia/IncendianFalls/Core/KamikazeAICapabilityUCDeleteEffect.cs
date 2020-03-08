using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeAICapabilityUCDeleteEffect : IKamikazeAICapabilityUCEffect {
  public readonly int id;
  public KamikazeAICapabilityUCDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeAICapabilityUCEffect.id => id;
  public void visit(IKamikazeAICapabilityUCEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCDeleteEffect(this);
  }
}

}
