using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RavaNestTTCCreateEffect : IRavaNestTTCEffect {
  public readonly int id;
  public readonly RavaNestTTCIncarnation incarnation;
  public RavaNestTTCCreateEffect(int id, RavaNestTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IRavaNestTTCEffect.id => id;
  public void visitIRavaNestTTCEffect(IRavaNestTTCEffectVisitor visitor) {
    visitor.visitRavaNestTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaNestTTCEffect(this);
  }
}

}
