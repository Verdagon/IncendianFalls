using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffTTCMutSetAddEffect : ICliffTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CliffTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICliffTTCMutSetEffect.id => id;
  public void visit(ICliffTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetAddEffect(this);
  }
}

}
