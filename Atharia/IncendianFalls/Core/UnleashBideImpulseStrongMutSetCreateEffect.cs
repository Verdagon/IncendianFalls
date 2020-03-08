using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnleashBideImpulseStrongMutSetCreateEffect : IUnleashBideImpulseStrongMutSetEffect {
  public readonly int id;
  public UnleashBideImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IUnleashBideImpulseStrongMutSetEffect.id => id;
  public void visit(IUnleashBideImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseStrongMutSetCreateEffect(this);
  }
}

}
