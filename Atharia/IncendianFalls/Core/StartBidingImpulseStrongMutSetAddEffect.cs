using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StartBidingImpulseStrongMutSetAddEffect : IStartBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public StartBidingImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IStartBidingImpulseStrongMutSetEffect.id => id;
  public void visitIStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetEffect(this);
  }
}

}
