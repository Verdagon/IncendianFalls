using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireBombImpulseStrongMutSetEffectVisitor {
  void visitFireBombImpulseStrongMutSetCreateEffect(FireBombImpulseStrongMutSetCreateEffect effect);
  void visitFireBombImpulseStrongMutSetDeleteEffect(FireBombImpulseStrongMutSetDeleteEffect effect);
  void visitFireBombImpulseStrongMutSetAddEffect(FireBombImpulseStrongMutSetAddEffect effect);
  void visitFireBombImpulseStrongMutSetRemoveEffect(FireBombImpulseStrongMutSetRemoveEffect effect);
}
         
}
