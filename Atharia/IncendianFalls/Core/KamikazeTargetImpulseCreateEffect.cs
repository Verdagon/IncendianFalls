using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetImpulseCreateEffect : IKamikazeTargetImpulseEffect {
  public readonly int id;
  public KamikazeTargetImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetImpulseEffect.id => id;
  public void visit(IKamikazeTargetImpulseEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseCreateEffect(this);
  }
}

}
