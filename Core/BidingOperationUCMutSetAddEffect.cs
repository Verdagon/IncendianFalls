using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BidingOperationUCMutSetAddEffect : IBidingOperationUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BidingOperationUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBidingOperationUCMutSetEffect.id => id;
  public void visit(IBidingOperationUCMutSetEffectVisitor visitor) {
    visitor.visitBidingOperationUCMutSetAddEffect(this);
  }
}

}
