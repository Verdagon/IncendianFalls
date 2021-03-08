using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KamikazeTargetTTCIncarnation : IKamikazeTargetTTCEffectVisitor {
  public readonly int capability;
  public KamikazeTargetTTCIncarnation(
      int capability) {
    this.capability = capability;
  }
  public KamikazeTargetTTCIncarnation Copy() {
    return new KamikazeTargetTTCIncarnation(
capability    );
  }

  public void visitKamikazeTargetTTCCreateEffect(KamikazeTargetTTCCreateEffect e) {}
  public void visitKamikazeTargetTTCDeleteEffect(KamikazeTargetTTCDeleteEffect e) {}

  public void ApplyEffect(IKamikazeTargetTTCEffect effect) { effect.visitIKamikazeTargetTTCEffect(this); }
}

}
