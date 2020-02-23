using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefendImpulseEffect {
  int id { get; }
  void visit(IDefendImpulseEffectVisitor visitor);
}
       
}
