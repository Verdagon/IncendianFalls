using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetTTCMutSetRemoveEffect : IKamikazeTargetTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public KamikazeTargetTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IKamikazeTargetTTCMutSetEffect.id => id;
  public void visit(IKamikazeTargetTTCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetRemoveEffect(this);
  }
}

}
