using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetTTCCreateEffect : IKamikazeTargetTTCEffect {
  public readonly int id;
  public readonly KamikazeTargetTTCIncarnation incarnation;
  public KamikazeTargetTTCCreateEffect(int id, KamikazeTargetTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IKamikazeTargetTTCEffect.id => id;
  public void visitIKamikazeTargetTTCEffect(IKamikazeTargetTTCEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCEffect(this);
  }
}

}
