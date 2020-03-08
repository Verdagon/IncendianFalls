using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetTTCCreateEffect : IKamikazeTargetTTCEffect {
  public readonly int id;
  public KamikazeTargetTTCCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetTTCEffect.id => id;
  public void visit(IKamikazeTargetTTCEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCCreateEffect(this);
  }
}

}
