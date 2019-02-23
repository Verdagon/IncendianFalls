using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BidingOperationUCMutSetRemoveEffect : IBidingOperationUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BidingOperationUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBidingOperationUCMutSetEffect.id => id;
  public void visit(IBidingOperationUCMutSetEffectVisitor visitor) {
    visitor.visitBidingOperationUCMutSetRemoveEffect(this);
  }
}

}
