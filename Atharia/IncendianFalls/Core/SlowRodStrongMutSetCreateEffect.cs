using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodStrongMutSetCreateEffect : ISlowRodStrongMutSetEffect {
  public readonly int id;
  public SlowRodStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISlowRodStrongMutSetEffect.id => id;
  public void visit(ISlowRodStrongMutSetEffectVisitor visitor) {
    visitor.visitSlowRodStrongMutSetCreateEffect(this);
  }
}

}