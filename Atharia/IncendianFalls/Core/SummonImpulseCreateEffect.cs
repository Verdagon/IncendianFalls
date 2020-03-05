using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SummonImpulseCreateEffect : ISummonImpulseEffect {
  public readonly int id;
  public SummonImpulseCreateEffect(int id) {
    this.id = id;
  }
  int ISummonImpulseEffect.id => id;
  public void visit(ISummonImpulseEffectVisitor visitor) {
    visitor.visitSummonImpulseCreateEffect(this);
  }
}

}
