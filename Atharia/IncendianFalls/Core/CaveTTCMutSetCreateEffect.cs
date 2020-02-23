using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CaveTTCMutSetCreateEffect : ICaveTTCMutSetEffect {
  public readonly int id;
  public CaveTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICaveTTCMutSetEffect.id => id;
  public void visit(ICaveTTCMutSetEffectVisitor visitor) {
    visitor.visitCaveTTCMutSetCreateEffect(this);
  }
}

}
