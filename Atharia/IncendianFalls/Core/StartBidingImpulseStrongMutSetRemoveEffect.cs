using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StartBidingImpulseStrongMutSetRemoveEffect : IStartBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public StartBidingImpulseStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IStartBidingImpulseStrongMutSetEffect.id => id;
  public void visit(IStartBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetRemoveEffect(this);
  }
}

}
