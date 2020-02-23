using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaNestTTCMutSetDeleteEffect : IRavaNestTTCMutSetEffect {
  public readonly int id;
  public RavaNestTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IRavaNestTTCMutSetEffect.id => id;
  public void visit(IRavaNestTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetDeleteEffect(this);
  }
}

}
