using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IAttackImpulseEffect {
  int id { get; }
  void visit(IAttackImpulseEffectVisitor visitor);
}
       
}
