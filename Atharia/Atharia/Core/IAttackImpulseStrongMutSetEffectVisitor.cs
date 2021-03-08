using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackImpulseStrongMutSetEffectVisitor {
  void visitAttackImpulseStrongMutSetCreateEffect(AttackImpulseStrongMutSetCreateEffect effect);
  void visitAttackImpulseStrongMutSetDeleteEffect(AttackImpulseStrongMutSetDeleteEffect effect);
  void visitAttackImpulseStrongMutSetAddEffect(AttackImpulseStrongMutSetAddEffect effect);
  void visitAttackImpulseStrongMutSetRemoveEffect(AttackImpulseStrongMutSetRemoveEffect effect);
}
         
}
