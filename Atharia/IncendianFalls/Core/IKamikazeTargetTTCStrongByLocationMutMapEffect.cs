using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKamikazeTargetTTCStrongByLocationMutMapEffect {
  int id { get; }
  void visit(IKamikazeTargetTTCStrongByLocationMutMapEffectVisitor visitor);
}

}
