using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnleashBideImpulseCreateEffect : IUnleashBideImpulseEffect {
  public readonly int id;
  public UnleashBideImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IUnleashBideImpulseEffect.id => id;
  public void visit(IUnleashBideImpulseEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseCreateEffect(this);
  }
}

}
