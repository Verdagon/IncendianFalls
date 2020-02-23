using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StoneTTCMutSetDeleteEffect : IStoneTTCMutSetEffect {
  public readonly int id;
  public StoneTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IStoneTTCMutSetEffect.id => id;
  public void visit(IStoneTTCMutSetEffectVisitor visitor) {
    visitor.visitStoneTTCMutSetDeleteEffect(this);
  }
}

}
