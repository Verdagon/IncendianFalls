using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffTTCCreateEffect : ICliffTTCEffect {
  public readonly int id;
  public CliffTTCCreateEffect(int id) {
    this.id = id;
  }
  int ICliffTTCEffect.id => id;
  public void visit(ICliffTTCEffectVisitor visitor) {
    visitor.visitCliffTTCCreateEffect(this);
  }
}

}
