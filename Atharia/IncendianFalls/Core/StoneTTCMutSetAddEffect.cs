using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetAddEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public StoneTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visit(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetAddEffect(this);
  }
}

}
