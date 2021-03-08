using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireTTCMutSetEffectVisitor {
  void visitFireTTCMutSetCreateEffect(FireTTCMutSetCreateEffect effect);
  void visitFireTTCMutSetDeleteEffect(FireTTCMutSetDeleteEffect effect);
  void visitFireTTCMutSetAddEffect(FireTTCMutSetAddEffect effect);
  void visitFireTTCMutSetRemoveEffect(FireTTCMutSetRemoveEffect effect);
}
         
}
