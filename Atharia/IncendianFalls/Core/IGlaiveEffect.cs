using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGlaiveEffect {
  int id { get; }
  void visit(IGlaiveEffectVisitor visitor);
}
       
}
