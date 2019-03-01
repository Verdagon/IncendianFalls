using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GlaiveCreateEffect : IGlaiveEffect {
  public readonly int id;
  public GlaiveCreateEffect(int id) {
    this.id = id;
  }
  int IGlaiveEffect.id => id;
  public void visit(IGlaiveEffectVisitor visitor) {
    visitor.visitGlaiveCreateEffect(this);
  }
}

}
