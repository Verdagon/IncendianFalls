using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBidingUCEffectVisitor {
  void visitBidingUCCreateEffect(BidingUCCreateEffect effect);
  void visitBidingUCDeleteEffect(BidingUCDeleteEffect effect);
}

}
