using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvolvifyAICapabilityUCMutSetEffectVisitor {
  void visitEvolvifyAICapabilityUCMutSetCreateEffect(EvolvifyAICapabilityUCMutSetCreateEffect effect);
  void visitEvolvifyAICapabilityUCMutSetDeleteEffect(EvolvifyAICapabilityUCMutSetDeleteEffect effect);
  void visitEvolvifyAICapabilityUCMutSetAddEffect(EvolvifyAICapabilityUCMutSetAddEffect effect);
  void visitEvolvifyAICapabilityUCMutSetRemoveEffect(EvolvifyAICapabilityUCMutSetRemoveEffect effect);
}
         
}
