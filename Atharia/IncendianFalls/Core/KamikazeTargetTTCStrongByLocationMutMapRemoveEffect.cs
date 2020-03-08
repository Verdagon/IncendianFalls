using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetTTCStrongByLocationMutMapRemoveEffect : IKamikazeTargetTTCStrongByLocationMutMapEffect {
  public readonly int id;
  public readonly Location key;
  public KamikazeTargetTTCStrongByLocationMutMapRemoveEffect(int id, Location key) {
    this.id = id;
    this.key = key;
  }
  int IKamikazeTargetTTCStrongByLocationMutMapEffect.id => id;
  public void visit(IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCStrongByLocationMutMapRemoveEffect(this);
  }
}

}
