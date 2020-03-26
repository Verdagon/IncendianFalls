using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavaNestTTCDeleteEffect : IRavaNestTTCEffect {
  public readonly int id;
  public RavaNestTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IRavaNestTTCEffect.id => id;
  public void visitIRavaNestTTCEffect(IRavaNestTTCEffectVisitor visitor) {
    visitor.visitRavaNestTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaNestTTCEffect(this);
  }
}

}
