using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct InertiaRingCreateEffect : IInertiaRingEffect {
  public readonly int id;
  public InertiaRingCreateEffect(int id) {
    this.id = id;
  }
  int IInertiaRingEffect.id => id;
  public void visit(IInertiaRingEffectVisitor visitor) {
    visitor.visitInertiaRingCreateEffect(this);
  }
}

}
