using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetImpulseStrongMutSetRemoveEffect : IKamikazeTargetImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeTargetImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeTargetImpulseStrongMutSetEffect.id => id;
  public void visitIKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
