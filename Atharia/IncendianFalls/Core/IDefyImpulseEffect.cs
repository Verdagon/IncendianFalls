using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefyImpulseEffect {
  int id { get; }
  void visit(IDefyImpulseEffectVisitor visitor);
}
       
}
