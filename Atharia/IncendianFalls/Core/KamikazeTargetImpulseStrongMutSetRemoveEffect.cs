using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetImpulseStrongMutSetRemoveEffect : IKamikazeTargetImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public KamikazeTargetImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IKamikazeTargetImpulseStrongMutSetEffect.id => id;
  public void visit(IKamikazeTargetImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetRemoveEffect(this);
  }
}

}
