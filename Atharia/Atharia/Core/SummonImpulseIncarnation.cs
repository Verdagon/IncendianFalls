using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SummonImpulseIncarnation : ISummonImpulseEffectVisitor {
  public readonly int weight;
  public readonly string blueprintName;
  public readonly Location location;
  public SummonImpulseIncarnation(
      int weight,
      string blueprintName,
      Location location) {
    this.weight = weight;
    this.blueprintName = blueprintName;
    this.location = location;
  }
  public SummonImpulseIncarnation Copy() {
    return new SummonImpulseIncarnation(
weight,
blueprintName,
location    );
  }

  public void visitSummonImpulseCreateEffect(SummonImpulseCreateEffect e) {}
  public void visitSummonImpulseDeleteEffect(SummonImpulseDeleteEffect e) {}



  public void ApplyEffect(ISummonImpulseEffect effect) { effect.visitISummonImpulseEffect(this); }
}

}
