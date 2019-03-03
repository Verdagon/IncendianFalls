using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUCMutSetEffectVisitor {
  void visitShieldingUCMutSetCreateEffect(ShieldingUCMutSetCreateEffect effect);
  void visitShieldingUCMutSetDeleteEffect(ShieldingUCMutSetDeleteEffect effect);
  void visitShieldingUCMutSetAddEffect(ShieldingUCMutSetAddEffect effect);
  void visitShieldingUCMutSetRemoveEffect(ShieldingUCMutSetRemoveEffect effect);
}
         
}
