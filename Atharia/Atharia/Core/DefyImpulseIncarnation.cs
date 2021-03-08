using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyImpulseIncarnation : IDefyImpulseEffectVisitor {
  public readonly int weight;
  public DefyImpulseIncarnation(
      int weight) {
    this.weight = weight;
  }
  public DefyImpulseIncarnation Copy() {
    return new DefyImpulseIncarnation(
weight    );
  }

  public void visitDefyImpulseCreateEffect(DefyImpulseCreateEffect e) {}
  public void visitDefyImpulseDeleteEffect(DefyImpulseDeleteEffect e) {}

  public void ApplyEffect(IDefyImpulseEffect effect) { effect.visitIDefyImpulseEffect(this); }
}

}
