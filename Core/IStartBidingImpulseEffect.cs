using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IStartBidingImpulseEffect {
  int id { get; }
  void visit(IStartBidingImpulseEffectVisitor visitor);
}
       
}
