using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUCWeakMutSetEffectVisitor {
  void visitShieldingUCWeakMutSetCreateEffect(ShieldingUCWeakMutSetCreateEffect effect);
  void visitShieldingUCWeakMutSetDeleteEffect(ShieldingUCWeakMutSetDeleteEffect effect);
  void visitShieldingUCWeakMutSetAddEffect(ShieldingUCWeakMutSetAddEffect effect);
  void visitShieldingUCWeakMutSetRemoveEffect(ShieldingUCWeakMutSetRemoveEffect effect);
}
         
}
