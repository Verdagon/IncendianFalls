using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BidingOperationUCDeleteEffect : IBidingOperationUCEffect {
  public readonly int id;
  public BidingOperationUCDeleteEffect(int id) {
    this.id = id;
  }
  int IBidingOperationUCEffect.id => id;
  public void visit(IBidingOperationUCEffectVisitor visitor) {
    visitor.visitBidingOperationUCDeleteEffect(this);
  }
}
       
}
