using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveTTCMutSetRemoveEffect : ICaveTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CaveTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICaveTTCMutSetEffect.id => id;
  public void visit(ICaveTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetRemoveEffect(this);
  }
}

}
