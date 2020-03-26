using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeJumpImpulseStrongMutSetAddEffect : IKamikazeJumpImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeJumpImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeJumpImpulseStrongMutSetEffect.id => id;
  public void visitIKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetEffect(this);
  }
}

}
