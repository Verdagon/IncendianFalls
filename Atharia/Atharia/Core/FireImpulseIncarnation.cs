using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireImpulseIncarnation : IFireImpulseEffectVisitor {
  public readonly int weight;
  public readonly int targetUnit;
  public FireImpulseIncarnation(
      int weight,
      int targetUnit) {
    this.weight = weight;
    this.targetUnit = targetUnit;
  }
  public FireImpulseIncarnation Copy() {
    return new FireImpulseIncarnation(
weight,
targetUnit    );
  }

  public void visitFireImpulseCreateEffect(FireImpulseCreateEffect e) {}
  public void visitFireImpulseDeleteEffect(FireImpulseDeleteEffect e) {}


  public void ApplyEffect(IFireImpulseEffect effect) { effect.visitIFireImpulseEffect(this); }
}

}
