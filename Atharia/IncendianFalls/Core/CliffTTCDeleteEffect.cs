using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffTTCDeleteEffect : ICliffTTCEffect {
  public readonly int id;
  public CliffTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ICliffTTCEffect.id => id;
  public void visitICliffTTCEffect(ICliffTTCEffectVisitor visitor) {
    visitor.visitCliffTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffTTCEffect(this);
  }
}

}
