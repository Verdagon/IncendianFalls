using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StartBidingImpulseStrongMutSetCreateEffect : IStartBidingImpulseStrongMutSetEffect {
  public readonly int id;
  public StartBidingImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IStartBidingImpulseStrongMutSetEffect.id => id;
  public void visit(IStartBidingImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitStartBidingImpulseStrongMutSetCreateEffect(this);
  }
}

}
