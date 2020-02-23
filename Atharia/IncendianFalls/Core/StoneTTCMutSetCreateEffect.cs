using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetCreateEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public StoneTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visit(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetCreateEffect(this);
  }
}

}
