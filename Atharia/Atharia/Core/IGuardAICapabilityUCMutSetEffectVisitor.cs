using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGuardAICapabilityUCMutSetEffectVisitor {
  void visitGuardAICapabilityUCMutSetCreateEffect(GuardAICapabilityUCMutSetCreateEffect effect);
  void visitGuardAICapabilityUCMutSetDeleteEffect(GuardAICapabilityUCMutSetDeleteEffect effect);
  void visitGuardAICapabilityUCMutSetAddEffect(GuardAICapabilityUCMutSetAddEffect effect);
  void visitGuardAICapabilityUCMutSetRemoveEffect(GuardAICapabilityUCMutSetRemoveEffect effect);
}
         
}
