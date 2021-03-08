using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackAICapabilityUCWeakMutSetEffectVisitor {
  void visitAttackAICapabilityUCWeakMutSetCreateEffect(AttackAICapabilityUCWeakMutSetCreateEffect effect);
  void visitAttackAICapabilityUCWeakMutSetDeleteEffect(AttackAICapabilityUCWeakMutSetDeleteEffect effect);
  void visitAttackAICapabilityUCWeakMutSetAddEffect(AttackAICapabilityUCWeakMutSetAddEffect effect);
  void visitAttackAICapabilityUCWeakMutSetRemoveEffect(AttackAICapabilityUCWeakMutSetRemoveEffect effect);
}
         
}
