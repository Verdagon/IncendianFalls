using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InertiaRingMutSetAddEffect : IInertiaRingMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public InertiaRingMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IInertiaRingMutSetEffect.id => id;
  public void visit(IInertiaRingMutSetEffectVisitor visitor) {
    visitor.visitInertiaRingMutSetAddEffect(this);
  }
}

}
