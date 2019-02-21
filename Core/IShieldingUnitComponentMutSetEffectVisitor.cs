using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IShieldingUnitComponentMutSetEffectVisitor {
  void visitShieldingUnitComponentMutSetCreateEffect(ShieldingUnitComponentMutSetCreateEffect effect);
  void visitShieldingUnitComponentMutSetDeleteEffect(ShieldingUnitComponentMutSetDeleteEffect effect);
  void visitShieldingUnitComponentMutSetAddEffect(ShieldingUnitComponentMutSetAddEffect effect);
  void visitShieldingUnitComponentMutSetRemoveEffect(ShieldingUnitComponentMutSetRemoveEffect effect);
}
         
}
