using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BidingOperationUCSetChargeEffect : IBidingOperationUCEffect {
  public readonly int id;
  public readonly int newValue;
  public BidingOperationUCSetChargeEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IBidingOperationUCEffect.id => id;

  public void visit(IBidingOperationUCEffectVisitor visitor) {
    visitor.visitBidingOperationUCSetChargeEffect(this);
  }
}

}
