using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStartBidingImpulseStrongMutSetEffectVisitor {
  void visitStartBidingImpulseStrongMutSetCreateEffect(StartBidingImpulseStrongMutSetCreateEffect effect);
  void visitStartBidingImpulseStrongMutSetDeleteEffect(StartBidingImpulseStrongMutSetDeleteEffect effect);
  void visitStartBidingImpulseStrongMutSetAddEffect(StartBidingImpulseStrongMutSetAddEffect effect);
  void visitStartBidingImpulseStrongMutSetRemoveEffect(StartBidingImpulseStrongMutSetRemoveEffect effect);
}
         
}
