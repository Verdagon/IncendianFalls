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
  public void visit(IKamikazeJumpImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseStrongMutSetDeleteEffect(this);
  }
}

}
