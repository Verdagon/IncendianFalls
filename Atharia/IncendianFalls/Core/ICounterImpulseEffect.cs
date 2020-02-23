using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICounterImpulseEffect {
  int id { get; }
  void visit(ICounterImpulseEffectVisitor visitor);
}
       
}
