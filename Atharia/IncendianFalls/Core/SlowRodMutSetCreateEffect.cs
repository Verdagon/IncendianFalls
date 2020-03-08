using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct SlowRodMutSetCreateEffect : ISlowRodMutSetEffect {
  public readonly int id;
  public SlowRodMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ISlowRodMutSetEffect.id => id;
  public void visit(ISlowRodMutSetEffectVisitor visitor) {
    visitor.visitSlowRodMutSetCreateEffect(this);
  }
}

}
