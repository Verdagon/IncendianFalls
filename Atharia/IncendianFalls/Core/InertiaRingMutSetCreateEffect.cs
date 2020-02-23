using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InertiaRingMutSetCreateEffect : IInertiaRingMutSetEffect {
  public readonly int id;
  public InertiaRingMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IInertiaRingMutSetEffect.id => id;
  public void visit(IInertiaRingMutSetEffectVisitor visitor) {
    visitor.visitInertiaRingMutSetCreateEffect(this);
  }
}

}
