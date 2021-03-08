using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBideAICapabilityUCMutSetEffectVisitor {
  void visitBideAICapabilityUCMutSetCreateEffect(BideAICapabilityUCMutSetCreateEffect effect);
  void visitBideAICapabilityUCMutSetDeleteEffect(BideAICapabilityUCMutSetDeleteEffect effect);
  void visitBideAICapabilityUCMutSetAddEffect(BideAICapabilityUCMutSetAddEffect effect);
  void visitBideAICapabilityUCMutSetRemoveEffect(BideAICapabilityUCMutSetRemoveEffect effect);
}
         
}
