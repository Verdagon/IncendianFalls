using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BidingOperationUCCreateEffect : IBidingOperationUCEffect {
  public readonly int id;
  public BidingOperationUCCreateEffect(int id) {
    this.id = id;
  }
  int IBidingOperationUCEffect.id => id;
  public void visit(IBidingOperationUCEffectVisitor visitor) {
    visitor.visitBidingOperationUCCreateEffect(this);
  }
}

}
