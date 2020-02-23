using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRavaNestTTCEffect {
  int id { get; }
  void visit(IRavaNestTTCEffectVisitor visitor);
}
       
}
