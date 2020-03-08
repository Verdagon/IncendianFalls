using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SlowRodCreateEffect : ISlowRodEffect {
  public readonly int id;
  public SlowRodCreateEffect(int id) {
    this.id = id;
  }
  int ISlowRodEffect.id => id;
  public void visit(ISlowRodEffectVisitor visitor) {
    visitor.visitSlowRodCreateEffect(this);
  }
}

}
