using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeJumpImpulseCreateEffect : IKamikazeJumpImpulseEffect {
  public readonly int id;
  public readonly KamikazeJumpImpulseIncarnation incarnation;
  public KamikazeJumpImpulseCreateEffect(int id, KamikazeJumpImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IKamikazeJumpImpulseEffect.id => id;
  public void visitIKamikazeJumpImpulseEffect(IKamikazeJumpImpulseEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeJumpImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
