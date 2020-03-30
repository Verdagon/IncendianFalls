using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeAICapabilityUCSetTargetByLocationEffect : IKamikazeAICapabilityUCEffect {
  public readonly int id;
  public readonly int newValue;
  public KamikazeAICapabilityUCSetTargetByLocationEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IKamikazeAICapabilityUCEffect.id => id;

  public void visitIKamikazeAICapabilityUCEffect(IKamikazeAICapabilityUCEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCSetTargetByLocationEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeAICapabilityUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
