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
  public void visit(IKamikazeTargetTTCMutSetEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCMutSetDeleteEffect(this);
  }
}

}
