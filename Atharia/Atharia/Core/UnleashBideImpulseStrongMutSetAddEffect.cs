using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct UnleashBideImpulseStrongMutSetAddEffect : IUnleashBideImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public UnleashBideImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IUnleashBideImpulseStrongMutSetEffect.id => id;
  public void visitIUnleashBideImpulseStrongMutSetEffect(IUnleashBideImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
