using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHoldPositionImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IHoldPositionImpulseStrongMutSetEffectVisitor visitor);
}

}
