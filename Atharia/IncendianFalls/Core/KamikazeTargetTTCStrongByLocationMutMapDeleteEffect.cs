using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetTTCStrongByLocationMutMapDeleteEffect : IKamikazeTargetTTCStrongByLocationMutMapEffect {
  public readonly int id;
  public KamikazeTargetTTCStrongByLocationMutMapDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetTTCStrongByLocationMutMapEffect.id => id;
  public void visit(IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCStrongByLocationMutMapDeleteEffect(this);
  }
}

}
