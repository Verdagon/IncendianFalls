using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRavaArcanaLevelLinkerTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIRavaArcanaLevelLinkerTTCMutSetEffect(IRavaArcanaLevelLinkerTTCMutSetEffectVisitor visitor);
}

}
