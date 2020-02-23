using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICaveTTCEffect {
  int id { get; }
  void visit(ICaveTTCEffectVisitor visitor);
}
       
}
