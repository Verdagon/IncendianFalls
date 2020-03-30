using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnleashBideImpulseStrongMutSetRemoveEffect : IUnleashBideImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public UnleashBideImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IUnleashBideImpulseStrongMutSetEffect.id => id;
  public void visitIUnleashBideImpulseStrongMutSetEffect(IUnleashBideImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
