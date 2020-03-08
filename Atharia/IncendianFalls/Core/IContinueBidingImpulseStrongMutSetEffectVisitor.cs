using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IContinueBidingImpulseStrongMutSetEffectVisitor {
  void visitContinueBidingImpulseStrongMutSetCreateEffect(ContinueBidingImpulseStrongMutSetCreateEffect effect);
  void visitContinueBidingImpulseStrongMutSetDeleteEffect(ContinueBidingImpulseStrongMutSetDeleteEffect effect);
  void visitContinueBidingImpulseStrongMutSetAddEffect(ContinueBidingImpulseStrongMutSetAddEffect effect);
  void visitContinueBidingImpulseStrongMutSetRemoveEffect(ContinueBidingImpulseStrongMutSetRemoveEffect effect);
}
         
}
