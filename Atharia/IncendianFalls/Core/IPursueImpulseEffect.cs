using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPursueImpulseEffect {
  int id { get; }
  void visit(IPursueImpulseEffectVisitor visitor);
}
       
}
