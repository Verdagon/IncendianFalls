using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BidingOperationUCCreateEffect : IBidingOperationUCEffect {
  public readonly int id;
  public readonly BidingOperationUCIncarnation incarnation;
  public BidingOperationUCCreateEffect(
      int id,
      BidingOperationUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBidingOperationUCEffect.id => id;
  public void visit(IBidingOperationUCEffectVisitor visitor) {
    visitor.visitBidingOperationUCCreateEffect(this);
  }
}
       
}
