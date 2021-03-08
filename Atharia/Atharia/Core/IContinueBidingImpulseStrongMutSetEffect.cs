using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IContinueBidingImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIContinueBidingImpulseStrongMutSetEffect(IContinueBidingImpulseStrongMutSetEffectVisitor visitor);
}

}
