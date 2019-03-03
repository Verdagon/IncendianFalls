using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface INoImpulseEffect {
  int id { get; }
  void visit(INoImpulseEffectVisitor visitor);
}
       
}
