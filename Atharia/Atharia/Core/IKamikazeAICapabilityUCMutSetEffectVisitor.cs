using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeAICapabilityUCMutSetEffectVisitor {
  void visitKamikazeAICapabilityUCMutSetCreateEffect(KamikazeAICapabilityUCMutSetCreateEffect effect);
  void visitKamikazeAICapabilityUCMutSetDeleteEffect(KamikazeAICapabilityUCMutSetDeleteEffect effect);
  void visitKamikazeAICapabilityUCMutSetAddEffect(KamikazeAICapabilityUCMutSetAddEffect effect);
  void visitKamikazeAICapabilityUCMutSetRemoveEffect(KamikazeAICapabilityUCMutSetRemoveEffect effect);
}
         
}
