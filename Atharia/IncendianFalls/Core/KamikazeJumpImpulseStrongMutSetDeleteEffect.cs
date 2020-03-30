using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeJumpImpulseStrongMutSetDeleteEffect : IKamikazeJumpImpulseStrongMutSetEffect {
  public readonly int id;
  public KamikazeJumpImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeJumpImpulseStrongMutSetEffect.id => id;
  public void visitIKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
