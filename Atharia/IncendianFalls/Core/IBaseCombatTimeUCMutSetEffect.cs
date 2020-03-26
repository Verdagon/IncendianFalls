using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseCombatTimeUCMutSetEffect : IEffect {
  int id { get; }
  void visitIBaseCombatTimeUCMutSetEffect(IBaseCombatTimeUCMutSetEffectVisitor visitor);
}

}
