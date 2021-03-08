using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackImpulseEffectVisitor {
  void visitAttackImpulseCreateEffect(AttackImpulseCreateEffect effect);
  void visitAttackImpulseDeleteEffect(AttackImpulseDeleteEffect effect);
}

}
