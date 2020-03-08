using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireBombImpulseEffect {
  int id { get; }
  void visit(IFireBombImpulseEffectVisitor visitor);
}
       
}
