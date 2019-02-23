using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBideImpulseEffect {
  int id { get; }
  void visit(IBideImpulseEffectVisitor visitor);
}
       
}
