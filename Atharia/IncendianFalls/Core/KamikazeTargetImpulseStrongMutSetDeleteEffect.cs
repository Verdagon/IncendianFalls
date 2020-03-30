using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetImpulseStrongMutSetDeleteEffect : IKamikazeTargetImpulseStrongMutSetEffect {
  public readonly int id;
  public KamikazeTargetImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetImpulseStrongMutSetEffect.id => id;
  public void visitIKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
