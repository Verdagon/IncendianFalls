using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IRavaArcanaLevelLinkerTTCEffect : IEffect {
  int id { get; }
  void visitIRavaArcanaLevelLinkerTTCEffect(IRavaArcanaLevelLinkerTTCEffectVisitor visitor);
}
       
}
