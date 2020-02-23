using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetRemoveEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public StoneTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visit(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetRemoveEffect(this);
  }
}

}
