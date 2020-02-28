using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InertiaRingStrongMutSetRemoveEffect : IInertiaRingStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public InertiaRingStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IInertiaRingStrongMutSetEffect.id => id;
  public void visit(IInertiaRingStrongMutSetEffectVisitor visitor) {
    visitor.visitInertiaRingStrongMutSetRemoveEffect(this);
  }
}

}
