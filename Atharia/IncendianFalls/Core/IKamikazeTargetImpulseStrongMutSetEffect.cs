using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIKamikazeTargetImpulseStrongMutSetEffect(IKamikazeTargetImpulseStrongMutSetEffectVisitor visitor);
}

}
