using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMoveImpulseEffect {
  int id { get; }
  void visit(IMoveImpulseEffectVisitor visitor);
}
       
}
