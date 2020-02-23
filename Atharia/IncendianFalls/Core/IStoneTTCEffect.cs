using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IStoneTTCEffect {
  int id { get; }
  void visit(IStoneTTCEffectVisitor visitor);
}
       
}
