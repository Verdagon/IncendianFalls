using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeJumpImpulseStrongMutSetCreateEffect : IKamikazeJumpImpulseStrongMutSetEffect {
  public readonly int id;
  public KamikazeJumpImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeJumpImpulseStrongMutSetEffect.id => id;
  public void visit(IKamikazeJumpImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetCreateEffect(this);
  }
}

}
