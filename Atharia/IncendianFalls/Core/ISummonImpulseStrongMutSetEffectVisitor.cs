using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonImpulseStrongMutSetEffectVisitor {
  void visitSummonImpulseStrongMutSetCreateEffect(SummonImpulseStrongMutSetCreateEffect effect);
  void visitSummonImpulseStrongMutSetDeleteEffect(SummonImpulseStrongMutSetDeleteEffect effect);
  void visitSummonImpulseStrongMutSetAddEffect(SummonImpulseStrongMutSetAddEffect effect);
  void visitSummonImpulseStrongMutSetRemoveEffect(SummonImpulseStrongMutSetRemoveEffect effect);
}
         
}
