using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetTTCStrongByLocationMutMapAddEffect : IKamikazeTargetTTCStrongByLocationMutMapEffect {
  public readonly int id;
  public readonly Location key;
  public readonly int value;
  public KamikazeTargetTTCStrongByLocationMutMapAddEffect(int id, Location key, int value) {
    this.id = id;
    this.key = key;
    this.value = value;
  }
  int IKamikazeTargetTTCStrongByLocationMutMapEffect.id => id;
  public void visitIKamikazeTargetTTCStrongByLocationMutMapEffect(IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCStrongByLocationMutMapAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCStrongByLocationMutMapEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
