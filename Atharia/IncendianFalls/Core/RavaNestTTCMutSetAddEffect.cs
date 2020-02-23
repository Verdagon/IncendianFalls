using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaNestTTCMutSetAddEffect : IRavaNestTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public RavaNestTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IRavaNestTTCMutSetEffect.id => id;
  public void visit(IRavaNestTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetAddEffect(this);
  }
}

}
