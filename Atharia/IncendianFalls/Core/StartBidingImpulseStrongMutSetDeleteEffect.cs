using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StartBidingImpulseStrongMutSetDeleteEffect : IStartBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public StartBidingImpulseStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IStartBidingImpulseStrongMutSetEffect.id => id;
  public void visit(IStartBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetDeleteEffect(this);
  }
}

}
