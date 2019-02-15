using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IAttackDirectiveEffect {
  int id { get; }
  void visit(IAttackDirectiveEffectVisitor visitor);
}
       
}
