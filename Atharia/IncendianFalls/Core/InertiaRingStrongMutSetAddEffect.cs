using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InertiaRingStrongMutSetAddEffect : IInertiaRingStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public InertiaRingStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IInertiaRingStrongMutSetEffect.id => id;
  public void visit(IInertiaRingStrongMutSetEffectVisitor visitor) {
    visitor.visitInertiaRingStrongMutSetAddEffect(this);
  }
}

}
