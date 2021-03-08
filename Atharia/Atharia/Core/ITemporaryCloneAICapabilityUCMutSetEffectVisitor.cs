using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneAICapabilityUCMutSetEffectVisitor {
  void visitTemporaryCloneAICapabilityUCMutSetCreateEffect(TemporaryCloneAICapabilityUCMutSetCreateEffect effect);
  void visitTemporaryCloneAICapabilityUCMutSetDeleteEffect(TemporaryCloneAICapabilityUCMutSetDeleteEffect effect);
  void visitTemporaryCloneAICapabilityUCMutSetAddEffect(TemporaryCloneAICapabilityUCMutSetAddEffect effect);
  void visitTemporaryCloneAICapabilityUCMutSetRemoveEffect(TemporaryCloneAICapabilityUCMutSetRemoveEffect effect);
}
         
}
