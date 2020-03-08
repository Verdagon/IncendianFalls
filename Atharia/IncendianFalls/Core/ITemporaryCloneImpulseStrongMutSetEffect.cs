using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneImpulseStrongMutSetEffect {
  int id { get; }
  void visit(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor);
}

}
