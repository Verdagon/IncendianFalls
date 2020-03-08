using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnleashBideImpulseStrongMutSetRemoveEffect : IUnleashBideImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public UnleashBideImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IUnleashBideImpulseStrongMutSetEffect.id => id;
  public void visit(IUnleashBideImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseStrongMutSetRemoveEffect(this);
  }
}

}
