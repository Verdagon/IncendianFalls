using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnleashBideImpulseIncarnation : IUnleashBideImpulseEffectVisitor {
  public readonly int weight;
  public UnleashBideImpulseIncarnation(
      int weight) {
    this.weight = weight;
  }
  public UnleashBideImpulseIncarnation Copy() {
    return new UnleashBideImpulseIncarnation(
weight    );
  }

  public void visitUnleashBideImpulseCreateEffect(UnleashBideImpulseCreateEffect e) {}
  public void visitUnleashBideImpulseDeleteEffect(UnleashBideImpulseDeleteEffect e) {}

  public void ApplyEffect(IUnleashBideImpulseEffect effect) { effect.visitIUnleashBideImpulseEffect(this); }
}

}
