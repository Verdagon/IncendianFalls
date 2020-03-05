using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISorcerousUCEffect {
  int id { get; }
  void visit(ISorcerousUCEffectVisitor visitor);
}
       
}
