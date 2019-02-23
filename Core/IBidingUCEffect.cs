using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBidingUCEffect {
  int id { get; }
  void visit(IBidingUCEffectVisitor visitor);
}
       
}
