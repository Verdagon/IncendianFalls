using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBidingOperationUCEffectVisitor {
  void visitBidingOperationUCCreateEffect(BidingOperationUCCreateEffect effect);
  void visitBidingOperationUCDeleteEffect(BidingOperationUCDeleteEffect effect);
}

}
