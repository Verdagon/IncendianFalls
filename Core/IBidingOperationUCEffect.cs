using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBidingOperationUCEffect {
  int id { get; }
  void visit(IBidingOperationUCEffectVisitor visitor);
}
       
}
