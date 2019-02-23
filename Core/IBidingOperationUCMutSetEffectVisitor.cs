using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBidingOperationUCMutSetEffectVisitor {
  void visitBidingOperationUCMutSetCreateEffect(BidingOperationUCMutSetCreateEffect effect);
  void visitBidingOperationUCMutSetDeleteEffect(BidingOperationUCMutSetDeleteEffect effect);
  void visitBidingOperationUCMutSetAddEffect(BidingOperationUCMutSetAddEffect effect);
  void visitBidingOperationUCMutSetRemoveEffect(BidingOperationUCMutSetRemoveEffect effect);
}
         
}
