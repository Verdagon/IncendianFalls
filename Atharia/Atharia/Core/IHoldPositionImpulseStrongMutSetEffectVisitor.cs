using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHoldPositionImpulseStrongMutSetEffectVisitor {
  void visitHoldPositionImpulseStrongMutSetCreateEffect(HoldPositionImpulseStrongMutSetCreateEffect effect);
  void visitHoldPositionImpulseStrongMutSetDeleteEffect(HoldPositionImpulseStrongMutSetDeleteEffect effect);
  void visitHoldPositionImpulseStrongMutSetAddEffect(HoldPositionImpulseStrongMutSetAddEffect effect);
  void visitHoldPositionImpulseStrongMutSetRemoveEffect(HoldPositionImpulseStrongMutSetRemoveEffect effect);
}
         
}
