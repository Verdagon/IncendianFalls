using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BidingUCDeleteEffect : IBidingUCEffect {
  public readonly int id;
  public BidingUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBidingUCEffect.id => id;
  public void visit(IBidingUCEffectVisitor visitor) {
    visitor.visitBidingUCDeleteEffect(this);
  }
}
       
}
