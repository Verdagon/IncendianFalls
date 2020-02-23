using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InertiaRingMutSetDeleteEffect : IInertiaRingMutSetEffect {
  public readonly int id;
  public InertiaRingMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IInertiaRingMutSetEffect.id => id;
  public void visit(IInertiaRingMutSetEffectVisitor visitor) {
    visitor.visitInertiaRingMutSetDeleteEffect(this);
  }
}

}
