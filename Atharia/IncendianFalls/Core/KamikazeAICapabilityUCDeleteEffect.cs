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
  public void visitIKamikazeAICapabilityUCEffect(IKamikazeAICapabilityUCEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
