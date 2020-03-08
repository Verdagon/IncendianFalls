using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeJumpImpulseStrongMutSetRemoveEffect : IKamikazeJumpImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public KamikazeJumpImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IKamikazeJumpImpulseStrongMutSetEffect.id => id;
  public void visit(IKamikazeJumpImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetRemoveEffect(this);
  }
}

}
