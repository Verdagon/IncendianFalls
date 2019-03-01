using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BidingOperationUCMutSetCreateEffect : IBidingOperationUCMutSetEffect {
  public readonly int id;
  public BidingOperationUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBidingOperationUCMutSetEffect.id => id;
  public void visit(IBidingOperationUCMutSetEffectVisitor visitor) {
    visitor.visitBidingOperationUCMutSetCreateEffect(this);
  }
}

}
