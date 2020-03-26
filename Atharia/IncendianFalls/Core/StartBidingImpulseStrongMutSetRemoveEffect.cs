using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StartBidingImpulseStrongMutSetRemoveEffect : IStartBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public StartBidingImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IStartBidingImpulseStrongMutSetEffect.id => id;
  public void visitIStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetEffect(this);
  }
}

}
