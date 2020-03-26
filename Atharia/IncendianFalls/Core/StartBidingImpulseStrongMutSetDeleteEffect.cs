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
  public void visitIStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetEffect(this);
  }
}

}
