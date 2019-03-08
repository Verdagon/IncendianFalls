using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct InertiaRingDeleteEffect : IInertiaRingEffect {
  public readonly int id;
  public InertiaRingDeleteEffect(int id) {
    this.id = id;
  }
  int IInertiaRingEffect.id => id;
  public void visit(IInertiaRingEffectVisitor visitor) {
    visitor.visitInertiaRingDeleteEffect(this);
  }
}

}
