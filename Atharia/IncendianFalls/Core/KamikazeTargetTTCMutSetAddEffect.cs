using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetTTCMutSetAddEffect : IKamikazeTargetTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeTargetTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeTargetTTCMutSetEffect.id => id;
  public void visitIKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetEffect(this);
  }
}

}
