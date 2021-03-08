using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseCombatTimeUCEffectVisitor {
  void visitBaseCombatTimeUCCreateEffect(BaseCombatTimeUCCreateEffect effect);
  void visitBaseCombatTimeUCDeleteEffect(BaseCombatTimeUCDeleteEffect effect);
}

}
