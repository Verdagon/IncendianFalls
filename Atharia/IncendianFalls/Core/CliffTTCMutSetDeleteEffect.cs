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
  public void visitICliffTTCMutSetEffect(ICliffTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetEffect(this);
  }
}

}
