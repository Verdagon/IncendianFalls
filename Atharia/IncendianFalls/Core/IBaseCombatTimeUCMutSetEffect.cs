using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseCombatTimeUCMutSetEffect {
  int id { get; }
  void visit(IBaseCombatTimeUCMutSetEffectVisitor visitor);
}

}
