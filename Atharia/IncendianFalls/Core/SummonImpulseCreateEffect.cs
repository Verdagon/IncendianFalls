using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SummonImpulseCreateEffect : ISummonImpulseEffect {
  public readonly int id;
  public readonly SummonImpulseIncarnation incarnation;
  public SummonImpulseCreateEffect(int id, SummonImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ISummonImpulseEffect.id => id;
  public void visitISummonImpulseEffect(ISummonImpulseEffectVisitor visitor) {
    visitor.visitSummonImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonImpulseEffect(this);
  }
}

}
