using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface IRandEffect {
  int id { get; }
  void visit(IRandEffectVisitor visitor);
}
       
}
