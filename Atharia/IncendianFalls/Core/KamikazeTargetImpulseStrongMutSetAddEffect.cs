using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetImpulseStrongMutSetAddEffect : IKamikazeTargetImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeTargetImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeTargetImpulseStrongMutSetEffect.id => id;
  public void visitIKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
