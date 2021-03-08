using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITemporaryCloneImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitITemporaryCloneImpulseStrongMutSetEffect(ITemporaryCloneImpulseStrongMutSetEffectVisitor visitor);
}

}
