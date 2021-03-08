using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetImpulseStrongMutSetCreateEffect : IKamikazeTargetImpulseStrongMutSetEffect {
  public readonly int id;
  public KamikazeTargetImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetImpulseStrongMutSetEffect.id => id;
  public void visitIKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
