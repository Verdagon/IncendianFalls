using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireBombImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IFireBombImpulseStrongMutSetEffectVisitor visitor);
}

}
