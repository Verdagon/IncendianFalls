using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BidingOperationUCMutSetCreateEffect : IBidingOperationUCMutSetEffect {
  public readonly int id;
  public readonly BidingOperationUCMutSetIncarnation incarnation;
  public BidingOperationUCMutSetCreateEffect(
      int id,
      BidingOperationUCMutSetIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBidingOperationUCMutSetEffect.id => id;
  public void visit(IBidingOperationUCMutSetEffectVisitor visitor) {
    visitor.visitBidingOperationUCMutSetCreateEffect(this);
  }
}

}
