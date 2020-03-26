using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SummonImpulseDeleteEffect : ISummonImpulseEffect {
  public readonly int id;
  public SummonImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int ISummonImpulseEffect.id => id;
  public void visitISummonImpulseEffect(ISummonImpulseEffectVisitor visitor) {
    visitor.visitSummonImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSummonImpulseEffect(this);
  }
}

}
