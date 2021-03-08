using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeJumpImpulseIncarnation : IKamikazeJumpImpulseEffectVisitor {
  public readonly int weight;
  public readonly int capability;
  public readonly Location jumpTarget;
  public KamikazeJumpImpulseIncarnation(
      int weight,
      int capability,
      Location jumpTarget) {
    this.weight = weight;
    this.capability = capability;
    this.jumpTarget = jumpTarget;
  }
  public KamikazeJumpImpulseIncarnation Copy() {
    return new KamikazeJumpImpulseIncarnation(
weight,
capability,
jumpTarget    );
  }

  public void visitKamikazeJumpImpulseCreateEffect(KamikazeJumpImpulseCreateEffect e) {}
  public void visitKamikazeJumpImpulseDeleteEffect(KamikazeJumpImpulseDeleteEffect e) {}



  public void ApplyEffect(IKamikazeJumpImpulseEffect effect) { effect.visitIKamikazeJumpImpulseEffect(this); }
}

}
