using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetTTCMutSetCreateEffect : IKamikazeTargetTTCMutSetEffect {
  public readonly int id;
  public KamikazeTargetTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetTTCMutSetEffect.id => id;
  public void visitIKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetEffect(this);
  }
}

}
