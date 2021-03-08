using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseCombatTimeUCEffect : IEffect {
  int id { get; }
  void visitIBaseCombatTimeUCEffect(IBaseCombatTimeUCEffectVisitor visitor);
}
       
}
