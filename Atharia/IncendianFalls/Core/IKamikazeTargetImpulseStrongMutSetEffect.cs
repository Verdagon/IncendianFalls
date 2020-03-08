using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetImpulseStrongMutSetEffect {
  int id { get; }
  void visit(IKamikazeTargetImpulseStrongMutSetEffectVisitor visitor);
}

}
