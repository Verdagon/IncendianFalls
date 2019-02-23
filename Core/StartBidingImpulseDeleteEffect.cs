using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StartBidingImpulseDeleteEffect : IStartBidingImpulseEffect {
  public readonly int id;
  public StartBidingImpulseDeleteEffect(int id) {
    this.id = id;
  }
  int IStartBidingImpulseEffect.id => id;
  public void visit(IStartBidingImpulseEffectVisitor visitor) {
    visitor.visitStartBidingImpulseDeleteEffect(this);
  }
}
       
}
