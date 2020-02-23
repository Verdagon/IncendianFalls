using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveTTCMutSetAddEffect : ICaveTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public CaveTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ICaveTTCMutSetEffect.id => id;
  public void visit(ICaveTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetAddEffect(this);
  }
}

}
