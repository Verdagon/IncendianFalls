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
  public void visitICliffTTCMutSetEffect(ICliffTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
