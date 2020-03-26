using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeJumpImpulseDeleteEffect : IKamikazeJumpImpulseEffect {
  public readonly int id;
  public KamikazeJumpImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeJumpImpulseEffect.id => id;
  public void visitIKamikazeJumpImpulseEffect(IKamikazeJumpImpulseEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseEffect(this);
  }
}

}
