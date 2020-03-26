using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetImpulseCreateEffect : IKamikazeTargetImpulseEffect {
  public readonly int id;
  public readonly KamikazeTargetImpulseIncarnation incarnation;
  public KamikazeTargetImpulseCreateEffect(int id, KamikazeTargetImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IKamikazeTargetImpulseEffect.id => id;
  public void visitIKamikazeTargetImpulseEffect(IKamikazeTargetImpulseEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseEffect(this);
  }
}

}
