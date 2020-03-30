using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaNestTTCMutSetRemoveEffect : IRavaNestTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RavaNestTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRavaNestTTCMutSetEffect.id => id;
  public void visitIRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
