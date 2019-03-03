using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BidingOperationUCMutSetDeleteEffect : IBidingOperationUCMutSetEffect {
  public readonly int id;
  public BidingOperationUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBidingOperationUCMutSetEffect.id => id;
  public void visit(IBidingOperationUCMutSetEffectVisitor visitor) {
    visitor.visitBidingOperationUCMutSetDeleteEffect(this);
  }
}

}
