using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaNestTTCMutSetCreateEffect : IRavaNestTTCMutSetEffect {
  public readonly int id;
  public RavaNestTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IRavaNestTTCMutSetEffect.id => id;
  public void visit(IRavaNestTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetCreateEffect(this);
  }
}

}
