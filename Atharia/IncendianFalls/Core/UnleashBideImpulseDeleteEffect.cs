using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnleashBideImpulseDeleteEffect : IUnleashBideImpulseEffect {
  public readonly int id;
  public UnleashBideImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IUnleashBideImpulseEffect.id => id;
  public void visitIUnleashBideImpulseEffect(IUnleashBideImpulseEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseEffect(this);
  }
}

}
