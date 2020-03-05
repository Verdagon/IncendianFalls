using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseCombatTimeUCMutSetEffectVisitor {
  void visitBaseCombatTimeUCMutSetCreateEffect(BaseCombatTimeUCMutSetCreateEffect effect);
  void visitBaseCombatTimeUCMutSetDeleteEffect(BaseCombatTimeUCMutSetDeleteEffect effect);
  void visitBaseCombatTimeUCMutSetAddEffect(BaseCombatTimeUCMutSetAddEffect effect);
  void visitBaseCombatTimeUCMutSetRemoveEffect(BaseCombatTimeUCMutSetRemoveEffect effect);
}
         
}
