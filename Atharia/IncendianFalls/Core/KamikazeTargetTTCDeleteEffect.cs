using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct KamikazeTargetTTCDeleteEffect : IKamikazeTargetTTCEffect {
  public readonly int id;
  public KamikazeTargetTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IKamikazeTargetTTCEffect.id => id;
  public void visit(IKamikazeTargetTTCEffectVisitor visitor) {
    visitor.visitKamikazeTargetTTCDeleteEffect(this);
  }
}

}
