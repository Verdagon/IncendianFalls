using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackAICapabilityUCEffectVisitor {
  void visitAttackAICapabilityUCCreateEffect(AttackAICapabilityUCCreateEffect effect);
  void visitAttackAICapabilityUCDeleteEffect(AttackAICapabilityUCDeleteEffect effect);
  void visitAttackAICapabilityUCSetKillDirectiveEffect(AttackAICapabilityUCSetKillDirectiveEffect effect);
}

}
