using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeTargetTTCStrongByLocationMutMapEffect : IEffect {
  int id { get; }
  void visitIKamikazeTargetTTCStrongByLocationMutMapEffect(IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor visitor);
}

}
