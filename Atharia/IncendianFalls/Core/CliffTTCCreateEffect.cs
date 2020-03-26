using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffTTCCreateEffect : ICliffTTCEffect {
  public readonly int id;
  public readonly CliffTTCIncarnation incarnation;
  public CliffTTCCreateEffect(int id, CliffTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICliffTTCEffect.id => id;
  public void visitICliffTTCEffect(ICliffTTCEffectVisitor visitor) {
    visitor.visitCliffTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffTTCEffect(this);
  }
}

}
