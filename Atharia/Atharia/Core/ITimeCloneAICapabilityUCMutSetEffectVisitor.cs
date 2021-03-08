using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeCloneAICapabilityUCMutSetEffectVisitor {
  void visitTimeCloneAICapabilityUCMutSetCreateEffect(TimeCloneAICapabilityUCMutSetCreateEffect effect);
  void visitTimeCloneAICapabilityUCMutSetDeleteEffect(TimeCloneAICapabilityUCMutSetDeleteEffect effect);
  void visitTimeCloneAICapabilityUCMutSetAddEffect(TimeCloneAICapabilityUCMutSetAddEffect effect);
  void visitTimeCloneAICapabilityUCMutSetRemoveEffect(TimeCloneAICapabilityUCMutSetRemoveEffect effect);
}
         
}
