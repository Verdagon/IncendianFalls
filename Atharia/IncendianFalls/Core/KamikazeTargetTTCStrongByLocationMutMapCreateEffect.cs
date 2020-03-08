using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetTTCStrongByLocationMutMapCreateEffect : IKamikazeTargetTTCStrongByLocationMutMapEffect {
  public readonly int id;
  public KamikazeTargetTTCStrongByLocationMutMapCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetTTCStrongByLocationMutMapEffect.id => id;
  public void visit(IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCStrongByLocationMutMapCreateEffect(this);
  }
}

}
