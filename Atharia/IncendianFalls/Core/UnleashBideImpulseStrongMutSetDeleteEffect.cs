using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnleashBideImpulseStrongMutSetDeleteEffect : IUnleashBideImpulseStrongMutSetEffect {
  public readonly int id;
  public UnleashBideImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IUnleashBideImpulseStrongMutSetEffect.id => id;
  public void visit(IUnleashBideImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseStrongMutSetDeleteEffect(this);
  }
}

}
