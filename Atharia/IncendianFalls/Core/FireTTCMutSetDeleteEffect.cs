using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireTTCMutSetDeleteEffect : IFireTTCMutSetEffect {
  public readonly int id;
  public FireTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IFireTTCMutSetEffect.id => id;
  public void visit(IFireTTCMutSetEffectVisitor visitor) {
    visitor.visitFireTTCMutSetDeleteEffect(this);
  }
}

}
