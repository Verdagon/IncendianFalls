using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonAICapabilityUCMutSetEffectVisitor {
  void visitSummonAICapabilityUCMutSetCreateEffect(SummonAICapabilityUCMutSetCreateEffect effect);
  void visitSummonAICapabilityUCMutSetDeleteEffect(SummonAICapabilityUCMutSetDeleteEffect effect);
  void visitSummonAICapabilityUCMutSetAddEffect(SummonAICapabilityUCMutSetAddEffect effect);
  void visitSummonAICapabilityUCMutSetRemoveEffect(SummonAICapabilityUCMutSetRemoveEffect effect);
}
         
}
