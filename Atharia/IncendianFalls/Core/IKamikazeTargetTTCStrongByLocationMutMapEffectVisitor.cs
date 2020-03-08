using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor {
  void visitKamikazeTargetTTCStrongByLocationMutMapCreateEffect(KamikazeTargetTTCStrongByLocationMutMapCreateEffect effect);
  void visitKamikazeTargetTTCStrongByLocationMutMapDeleteEffect(KamikazeTargetTTCStrongByLocationMutMapDeleteEffect effect);
  void visitKamikazeTargetTTCStrongByLocationMutMapAddEffect(KamikazeTargetTTCStrongByLocationMutMapAddEffect effect);
  void visitKamikazeTargetTTCStrongByLocationMutMapRemoveEffect(KamikazeTargetTTCStrongByLocationMutMapRemoveEffect effect);
}
         
}
