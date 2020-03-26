using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireBombImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIFireBombImpulseStrongMutSetEffect(IFireBombImpulseStrongMutSetEffectVisitor visitor);
}

}
