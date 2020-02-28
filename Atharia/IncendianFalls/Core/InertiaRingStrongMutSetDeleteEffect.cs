using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct InertiaRingStrongMutSetDeleteEffect : IInertiaRingStrongMutSetEffect {
  public readonly int id;
  public InertiaRingStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IInertiaRingStrongMutSetEffect.id => id;
  public void visit(IInertiaRingStrongMutSetEffectVisitor visitor) {
    visitor.visitInertiaRingStrongMutSetDeleteEffect(this);
  }
}

}
