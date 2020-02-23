using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffTTCMutSetDeleteEffect : ICliffTTCMutSetEffect {
  public readonly int id;
  public CliffTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ICliffTTCMutSetEffect.id => id;
  public void visit(ICliffTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetDeleteEffect(this);
  }
}

}
