using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetTTCMutSetRemoveEffect : IKamikazeTargetTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public KamikazeTargetTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IKamikazeTargetTTCMutSetEffect.id => id;
  public void visitIKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetEffect(this);
  }
}

}
