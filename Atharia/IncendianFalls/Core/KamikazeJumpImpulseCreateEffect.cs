using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeJumpImpulseCreateEffect : IKamikazeJumpImpulseEffect {
  public readonly int id;
  public KamikazeJumpImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeJumpImpulseEffect.id => id;
  public void visit(IKamikazeJumpImpulseEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseCreateEffect(this);
  }
}

}
