using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavaNestTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIRavaNestTTCMutSetEffect(IRavaNestTTCMutSetEffectVisitor visitor);
}

}
