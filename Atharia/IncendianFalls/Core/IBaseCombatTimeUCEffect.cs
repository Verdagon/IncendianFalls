using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBaseCombatTimeUCEffect {
  int id { get; }
  void visit(IBaseCombatTimeUCEffectVisitor visitor);
}
       
}
