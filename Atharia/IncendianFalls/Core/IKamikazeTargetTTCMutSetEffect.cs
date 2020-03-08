using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeTargetTTCMutSetEffect {
  int id { get; }
  void visit(IKamikazeTargetTTCMutSetEffectVisitor visitor);
}

}
