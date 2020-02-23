using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CliffTTCMutSetRemoveEffect : ICliffTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CliffTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICliffTTCMutSetEffect.id => id;
  public void visit(ICliffTTCMutSetEffectVisitor visitor) {
    visitor.visitCliffTTCMutSetRemoveEffect(this);
  }
}

}
