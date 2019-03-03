using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StartBidingImpulseCreateEffect : IStartBidingImpulseEffect {
  public readonly int id;
  public StartBidingImpulseCreateEffect(int id) {
    this.id = id;
  }
  int IStartBidingImpulseEffect.id => id;
  public void visit(IStartBidingImpulseEffectVisitor visitor) {
    visitor.visitStartBidingImpulseCreateEffect(this);
  }
}

}
