using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetImpulseStrongMutSetEffectVisitor {
  void visitKamikazeTargetImpulseStrongMutSetCreateEffect(KamikazeTargetImpulseStrongMutSetCreateEffect effect);
  void visitKamikazeTargetImpulseStrongMutSetDeleteEffect(KamikazeTargetImpulseStrongMutSetDeleteEffect effect);
  void visitKamikazeTargetImpulseStrongMutSetAddEffect(KamikazeTargetImpulseStrongMutSetAddEffect effect);
  void visitKamikazeTargetImpulseStrongMutSetRemoveEffect(KamikazeTargetImpulseStrongMutSetRemoveEffect effect);
}
         
}
