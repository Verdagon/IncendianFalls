using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct UnleashBideImpulseCreateEffect : IUnleashBideImpulseEffect {
  public readonly int id;
  public readonly UnleashBideImpulseIncarnation incarnation;
  public UnleashBideImpulseCreateEffect(int id, UnleashBideImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUnleashBideImpulseEffect.id => id;
  public void visitIUnleashBideImpulseEffect(IUnleashBideImpulseEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitUnleashBideImpulseEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
