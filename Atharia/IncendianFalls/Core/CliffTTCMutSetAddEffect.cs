using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffTTCMutSetAddEffect : ICliffTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CliffTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICliffTTCMutSetEffect.id => id;
  public void visitICliffTTCMutSetEffect(ICliffTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
