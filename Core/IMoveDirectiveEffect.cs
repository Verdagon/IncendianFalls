using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IMoveDirectiveEffect {
  int id { get; }
  void visit(IMoveDirectiveEffectVisitor visitor);
}
       
}
