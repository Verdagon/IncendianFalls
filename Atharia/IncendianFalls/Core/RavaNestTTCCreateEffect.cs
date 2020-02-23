using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavaNestTTCCreateEffect : IRavaNestTTCEffect {
  public readonly int id;
  public RavaNestTTCCreateEffect(int id) {
    this.id = id;
  }
  int IRavaNestTTCEffect.id => id;
  public void visit(IRavaNestTTCEffectVisitor visitor) {
    visitor.visitRavaNestTTCCreateEffect(this);
  }
}

}
