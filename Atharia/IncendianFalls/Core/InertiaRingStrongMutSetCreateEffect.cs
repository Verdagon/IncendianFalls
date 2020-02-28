using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InertiaRingStrongMutSetCreateEffect : IInertiaRingStrongMutSetEffect {
  public readonly int id;
  public InertiaRingStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IInertiaRingStrongMutSetEffect.id => id;
  public void visit(IInertiaRingStrongMutSetEffectVisitor visitor) {
    visitor.visitInertiaRingStrongMutSetCreateEffect(this);
  }
}

}
