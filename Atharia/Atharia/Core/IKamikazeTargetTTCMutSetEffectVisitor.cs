using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetTTCMutSetEffectVisitor {
  void visitKamikazeTargetTTCMutSetCreateEffect(KamikazeTargetTTCMutSetCreateEffect effect);
  void visitKamikazeTargetTTCMutSetDeleteEffect(KamikazeTargetTTCMutSetDeleteEffect effect);
  void visitKamikazeTargetTTCMutSetAddEffect(KamikazeTargetTTCMutSetAddEffect effect);
  void visitKamikazeTargetTTCMutSetRemoveEffect(KamikazeTargetTTCMutSetRemoveEffect effect);
}
         
}
