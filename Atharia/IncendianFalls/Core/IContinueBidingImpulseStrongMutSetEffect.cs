using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IContinueBidingImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IContinueBidingImpulseStrongMutSetEffectVisitor visitor);
}

}
