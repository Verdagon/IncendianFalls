using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StartBidingImpulseCreateEffect : IStartBidingImpulseEffect {
  public readonly int id;
  public readonly StartBidingImpulseIncarnation incarnation;
  public StartBidingImpulseCreateEffect(int id, StartBidingImpulseIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IStartBidingImpulseEffect.id => id;
  public void visitIStartBidingImpulseEffect(IStartBidingImpulseEffectVisitor visitor) {
    visitor.visitStartBidingImpulseCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitStartBidingImpulseEffect(this);
  }
}

}
