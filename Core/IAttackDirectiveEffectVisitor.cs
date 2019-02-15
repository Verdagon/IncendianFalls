using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IAttackDirectiveEffectVisitor {
  void visitAttackDirectiveCreateEffect(AttackDirectiveCreateEffect effect);
  void visitAttackDirectiveDeleteEffect(AttackDirectiveDeleteEffect effect);
}

}
