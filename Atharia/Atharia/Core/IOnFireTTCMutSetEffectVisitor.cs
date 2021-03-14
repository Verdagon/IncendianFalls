using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireTTCMutSetEffectVisitor {
  void visitOnFireTTCMutSetCreateEffect(OnFireTTCMutSetCreateEffect effect);
  void visitOnFireTTCMutSetDeleteEffect(OnFireTTCMutSetDeleteEffect effect);
  void visitOnFireTTCMutSetAddEffect(OnFireTTCMutSetAddEffect effect);
  void visitOnFireTTCMutSetRemoveEffect(OnFireTTCMutSetRemoveEffect effect);
}
         
}
