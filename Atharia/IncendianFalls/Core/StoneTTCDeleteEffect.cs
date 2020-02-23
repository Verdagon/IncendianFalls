using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StoneTTCDeleteEffect : IStoneTTCEffect {
  public readonly int id;
  public StoneTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IStoneTTCEffect.id => id;
  public void visit(IStoneTTCEffectVisitor visitor) {
    visitor.visitStoneTTCDeleteEffect(this);
  }
}

}
