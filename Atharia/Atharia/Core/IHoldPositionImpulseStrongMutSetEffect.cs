using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHoldPositionImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIHoldPositionImpulseStrongMutSetEffect(IHoldPositionImpulseStrongMutSetEffectVisitor visitor);
}

}
