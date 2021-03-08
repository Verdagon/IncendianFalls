using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct RavaNestTTCMutSetAddEffect : IRavaNestTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public RavaNestTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IRavaNestTTCMutSetEffect.id => id;
  public void visitIRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRavaNestTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
