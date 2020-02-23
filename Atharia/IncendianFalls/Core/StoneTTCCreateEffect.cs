using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StoneTTCCreateEffect : IStoneTTCEffect {
  public readonly int id;
  public StoneTTCCreateEffect(int id) {
    this.id = id;
  }
  int IStoneTTCEffect.id => id;
  public void visit(IStoneTTCEffectVisitor visitor) {
    visitor.visitStoneTTCCreateEffect(this);
  }
}

}
