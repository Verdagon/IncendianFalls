using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISummonImpulseEffect {
  int id { get; }
  void visit(ISummonImpulseEffectVisitor visitor);
}
       
}
