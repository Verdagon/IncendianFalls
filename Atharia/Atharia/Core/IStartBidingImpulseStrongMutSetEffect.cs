using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStartBidingImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIStartBidingImpulseStrongMutSetEffect(IStartBidingImpulseStrongMutSetEffectVisitor visitor);
}

}
