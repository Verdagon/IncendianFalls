using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStartBidingImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IStartBidingImpulseStrongMutSetEffectVisitor visitor);
}

}
