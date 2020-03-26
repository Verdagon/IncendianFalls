using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct KamikazeTargetTTCMutSetDeleteEffect : IKamikazeTargetTTCMutSetEffect {
  public readonly int id;
  public KamikazeTargetTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetTTCMutSetEffect.id => id;
  public void visitIKamikazeTargetTTCMutSetEffect(IKamikazeTargetTTCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetEffect(this);
  }
}

}
