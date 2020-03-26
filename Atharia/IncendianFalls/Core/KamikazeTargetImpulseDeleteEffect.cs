using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetImpulseDeleteEffect : IKamikazeTargetImpulseEffect {
  public readonly int id;
  public KamikazeTargetImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetImpulseEffect.id => id;
  public void visitIKamikazeTargetImpulseEffect(IKamikazeTargetImpulseEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseEffect(this);
  }
}

}
