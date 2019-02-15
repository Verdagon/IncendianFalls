using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IRandEffect {
  int id { get; }
  void visit(IRandEffectVisitor visitor);
}
       
}
