using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IMireImpulseEffect {
  int id { get; }
  void visit(IMireImpulseEffectVisitor visitor);
}
       
}
