using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveTTCCreateEffect : ICaveTTCEffect {
  public readonly int id;
  public CaveTTCCreateEffect(int id) {
    this.id = id;
  }
  int ICaveTTCEffect.id => id;
  public void visit(ICaveTTCEffectVisitor visitor) {
    visitor.visitCaveTTCCreateEffect(this);
  }
}

}
