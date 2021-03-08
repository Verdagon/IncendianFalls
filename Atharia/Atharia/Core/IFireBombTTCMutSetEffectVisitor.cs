using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireBombTTCMutSetEffectVisitor {
  void visitFireBombTTCMutSetCreateEffect(FireBombTTCMutSetCreateEffect effect);
  void visitFireBombTTCMutSetDeleteEffect(FireBombTTCMutSetDeleteEffect effect);
  void visitFireBombTTCMutSetAddEffect(FireBombTTCMutSetAddEffect effect);
  void visitFireBombTTCMutSetRemoveEffect(FireBombTTCMutSetRemoveEffect effect);
}
         
}
