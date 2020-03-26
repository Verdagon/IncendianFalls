using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeJumpImpulseStrongMutSetRemoveEffect : IKamikazeJumpImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeJumpImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeJumpImpulseStrongMutSetEffect.id => id;
  public void visitIKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetEffect(this);
  }
}

}
