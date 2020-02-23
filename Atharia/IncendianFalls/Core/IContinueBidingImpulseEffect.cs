using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IContinueBidingImpulseEffect {
  int id { get; }
  void visit(IContinueBidingImpulseEffectVisitor visitor);
}
       
}
