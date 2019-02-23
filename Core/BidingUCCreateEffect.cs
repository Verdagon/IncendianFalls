using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BidingUCCreateEffect : IBidingUCEffect {
  public readonly int id;
  public readonly BidingUCIncarnation incarnation;
  public BidingUCCreateEffect(
      int id,
      BidingUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBidingUCEffect.id => id;
  public void visit(IBidingUCEffectVisitor visitor) {
    visitor.visitBidingUCCreateEffect(this);
  }
}
       
}
