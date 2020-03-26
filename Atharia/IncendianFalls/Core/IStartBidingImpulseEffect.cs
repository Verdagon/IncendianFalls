using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IStartBidingImpulseEffect : IEffect {
  int id { get; }
  void visitIStartBidingImpulseEffect(IStartBidingImpulseEffectVisitor visitor);
}
       
}
