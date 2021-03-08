using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBloodTTCMutSetEffectVisitor {
  void visitBloodTTCMutSetCreateEffect(BloodTTCMutSetCreateEffect effect);
  void visitBloodTTCMutSetDeleteEffect(BloodTTCMutSetDeleteEffect effect);
  void visitBloodTTCMutSetAddEffect(BloodTTCMutSetAddEffect effect);
  void visitBloodTTCMutSetRemoveEffect(BloodTTCMutSetRemoveEffect effect);
}
         
}
