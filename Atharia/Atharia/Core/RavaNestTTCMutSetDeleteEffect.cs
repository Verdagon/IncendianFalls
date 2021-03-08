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
  public void visitIRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
