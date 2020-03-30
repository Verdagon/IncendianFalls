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
  public void visitIRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
