using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffTTCMutSetCreateEffect : ICliffTTCMutSetEffect {
  public readonly int id;
  public CliffTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICliffTTCMutSetEffect.id => id;
  public void visit(ICliffTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetCreateEffect(this);
  }
}

}
